using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DatabaseProjectsTask
{

    // Project 1: E-commerce System

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation Property (One-to-Many with Product)
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Foreign Key & Navigation
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation Property (One-to-Many with Order)
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        // Foreign Key & Navigation
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    // Junction Entity for Many-to-Many (Order & Product)
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }


    // ==========================================
    // Project 2: Library System
    // ==========================================
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        // Navigation Property (One-to-Many with Book)
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;

        // Foreign Key & Navigation
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime MembershipDate { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

    // Junction Entity for Many-to-Many (Book & Borrower)
    public class Loan
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; } = null!;

        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }


    // ==========================================
    // Project 3: Health Care System
    // ==========================================
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }

    // Junction Entity for Many-to-Many (Patient & Doctor)
    public class Appointment
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public DateTime AppointmentDate { get; set; }
    }


    // ==========================================
    // DbContext Setup with Mappings
    // ==========================================
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. E-Commerce Many-to-Many Composite Key
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            // 2. Library Many-to-Many Composite Key
            modelBuilder.Entity<Loan>()
                .HasKey(l => new { l.BookId, l.BorrowerId, l.LoanDate });

            // 3. Health Care Many-to-Many Composite Key
            modelBuilder.Entity<Appointment>()
                .HasKey(a => new { a.PatientId, a.DoctorId, a.AppointmentDate });
        }
    }
}