using System;
using Microsoft.EntityFrameworkCore;
using QualcoOne.Models;

public class DataBase: DbContext
{
	public DataBase(DbContextOptions<DataBase> options) : base(options)
    {	}

    public DbSet<Citizen> Citizens { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Settlement> Settlements { get; set; }
    public DbSet<SettlementType> SettlementTypes { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<DbVersion> DbVersions { get; set; }
    public DbSet<Installation> Installations { get; set; }
    public DbSet<TempImportedFile> TempImportedFiles { get; set; }
    public DbSet<AppUser> AspNetUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //TempImportedFile
        var MB_TempImportedFile = modelBuilder.Entity<TempImportedFile>();

        MB_TempImportedFile.ToTable("TempImportedFile");

        //use t from Type of Column
        MB_TempImportedFile.HasKey(t => t.BillMunicipalityId);

        MB_TempImportedFile.Property(t => t.Vat)
            .HasMaxLength(10)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.LastName)
            .HasMaxLength(100)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.Email)
            .HasMaxLength(50)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.Phone)
            .HasMaxLength(20)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.CompleteAddress)
            .HasMaxLength(50)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.County)
            .HasMaxLength(50)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.BillMunicipalityId)
            .HasMaxLength(50)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.BillDescription)
            .HasMaxLength(50)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.Amount)
            .IsRequired();

        MB_TempImportedFile.Property(t => t.DueDate)
            .IsRequired();



        //Citizen
        var MB_Citizen = modelBuilder.Entity<Citizen>();

        MB_Citizen.ToTable("Citizen");

        MB_Citizen.HasKey(t => t.CitizenId);

        MB_Citizen.Property(t => t.CitizenId)
            .HasMaxLength(10)
            .IsRequired();

        MB_Citizen.Property(t => t.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        MB_Citizen.Property(t => t.LastName)
            .HasMaxLength(100)
            .IsRequired();

        MB_Citizen.Property(t => t.Email)
            .HasMaxLength(50)
            .IsRequired();

        MB_Citizen.Property(t => t.Phone)
            .HasMaxLength(20)
            .IsRequired();

        MB_Citizen.Property(t => t.CompleteAddress)
            .HasMaxLength(50)
            .IsRequired();

        MB_Citizen.Property(t => t.County)
            .HasMaxLength(50)
            .IsRequired();

        MB_Citizen.Property(t => t.Password)
            .HasMaxLength(20)
            .IsRequired();

        //Set References
        //One Citizen (c) has many Bill (b) so first step going from Citizen to Bill
        MB_Citizen.HasMany(c => c.Bills)
            //One Bill (b) has many Citizen (c) so second step going from Bill to Citizen
            .WithOne(b => b.Citizen)
            //set foreignkey 
            .HasForeignKey(b => b.CitizenId);


        //Bill
        var MB_Bill = modelBuilder.Entity<Bill>();

        MB_Bill.ToTable("Bill");

        //use t from Type of Column
        MB_Bill.HasKey(t => t.BillId);

        MB_Bill.Property(t => t.BillDescription)
            .HasMaxLength(50)
            .IsRequired();

        MB_Bill.Property(t => t.BillMunicipalityId)
            .HasMaxLength(50)
            .IsRequired();

        MB_Bill.Property(t => t.Amount)
            .IsRequired();

        MB_Bill.Property(t => t.DueDate)
            .IsRequired();

        //Set References
        //One Bill (b) has one or zero Payment (p) so first step going from Bill to Payment
        MB_Bill.HasOne(b => b.Payment)
            //One Payment (p) has one Bill (b) so second step going from Payment to Bill
            .WithOne(p => p.Bill)
            .HasForeignKey<Bill>(b => b.PaymentId)
            .IsRequired(false);

        //One Bill (b) has one or zero Settlement (p) so first step going from Bill to Settlement
        MB_Bill.HasOne(b => b.Settlement)
            //One Settlement (s) has many Bill (b) so second step going from Settlement to Bill
            .WithMany(s => s.Bills)
            //set foreignkey 
            .HasForeignKey(b => b.SettlementId)
            .IsRequired(false);

   
        //Payment
        var MB_Payment = modelBuilder.Entity<Payment>();

        MB_Payment.ToTable("Payment");

        //use t from Type of Column
        MB_Payment.HasKey(t => t.PaymentId);

        MB_Payment.Property(t => t.Paymentamount)
            .IsRequired();

        MB_Payment.Property(t => t.PaymentDateTime)
            .IsRequired();


        //Settlement
        var MB_Settlement = modelBuilder.Entity<Settlement>();

        MB_Settlement.ToTable("Settlement");

        //use t from Type of Column
        MB_Settlement.HasKey(t => t.SettlementId);

        MB_Settlement.Property(t => t.Downpayment)
           .IsRequired();

        MB_Settlement.Property(t => t.Installments)
            .IsRequired();

        MB_Settlement.Property(t => t.MonthlyAmount)
           .IsRequired();

        MB_Settlement.Property(t => t.SettlementDateTime)
            .IsRequired();

        //Set References
        //One Settlement (s) has one SettlementType (st) so first step going from Settlement to SettlementType
        MB_Settlement.HasOne(s => s.SettlementType)
            //One SettlementType (s) has many Settlement (s) so second step going from SettlementType to Settlement
            .WithMany(s => s.Settlements);


        //SettlementType
        var MB_SettlementType = modelBuilder.Entity<SettlementType>();

        MB_SettlementType.ToTable("SettlementType");

        //use t from Type of Column
        MB_SettlementType.HasKey(t => t.SettlementTypeId);

        MB_SettlementType.Property(t => t.DownpaymentPercentage)
            .IsRequired();

        MB_SettlementType.Property(t => t.NumOfInstallments)
            .IsRequired();

        MB_SettlementType.Property(t => t.Interest)
            .HasColumnType("decimal(5,4)")
            .IsRequired();


        //DbVersion
        var MB_DbVersion = modelBuilder.Entity<DbVersion>();

        MB_DbVersion.ToTable("DbVersion");

        //use t from Type of Column
        MB_DbVersion.HasKey(t => t.DbVersionId);

        MB_DbVersion.Property(t => t.DbVersionNum)
            .HasMaxLength(10)
            .IsRequired();


        //Installation
        var MB_Installation = modelBuilder.Entity<Installation>();

        MB_Installation.ToTable("Installation");

        //use t from Type of Column
        MB_Installation.HasKey(t => t.InstallationId);

        MB_Installation.Property(t => t.InstallationName)
            .HasMaxLength(50)
            .IsRequired();

        MB_Installation.Property(t => t.InstallationValue)
            .HasMaxLength(50)
            .IsRequired();

        
    }
}