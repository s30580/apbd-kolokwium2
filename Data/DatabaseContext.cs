using apbd_kolowium2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_kolowium2.Data;

public class DatabaseContext: DbContext
{
    public DbSet<Racer> Racers { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<RaceParticipation> RaceParticipations { get; set; }
    
    
    public DatabaseContext(){}
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Racer>(e =>
        {
            e.ToTable("Racer");
            e.HasKey(e => e.RacerId);
            e.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            e.Property(e => e.LastName).IsRequired().HasMaxLength(100);

            e.HasMany(e => e.RaceParticipations)
                .WithOne(e => e.Racer)
                .HasForeignKey(e => e.RacerId);
        });
        modelBuilder.Entity<Track>(e =>
        {
            e.ToTable("Track");
            e.HasKey(e => e.TrackId);
            e.Property(e => e.Name).IsRequired().HasMaxLength(100);
            e.Property(e => e.LengthInKm).IsRequired().HasPrecision(5, 2);

            e.HasMany(e => e.TrackRaces)
                .WithOne(e => e.Track)
                .HasForeignKey(e => e.TrackId);
        });
        modelBuilder.Entity<Race>(e =>
        {
            e.ToTable("Race");
            e.HasKey(e => e.RaceId);
            e.Property(e => e.Name).IsRequired().HasMaxLength(50);
            e.Property(e => e.Location).IsRequired().HasMaxLength(100);
            e.Property(e => e.Date).IsRequired();

            e.HasMany(e => e.TrackRaces)
                .WithOne(e => e.Race)
                .HasForeignKey(e => e.RaceId);
        });

        modelBuilder.Entity<RaceParticipation>(e =>
        {
            e.ToTable("Race_Participation");
            e.HasKey(key => new { key.RacerId, key.TrackRaceId });
            e.Property(e => e.FinishTimeInSeconds).IsRequired();
            e.Property(e => e.Position).IsRequired();
        });

        modelBuilder.Entity<TrackRace>(e =>
        {
            e.ToTable("Track_Race");
            e.HasKey(e => e.TrackRaceId);
            e.Property(e => e.Laps).IsRequired();
            e.Property(e => e.BestTimeInSeconds);
            
            e.HasMany(e=>e.RaceParticipations)
                .WithOne(e => e.TrackRace)
                .HasForeignKey(e => e.TrackRaceId);
        });
        
        modelBuilder.Entity<Racer>().HasData(
            new Racer { RacerId = 1, FirstName = "Lewis", LastName = "Hamiltion" }
        );
        modelBuilder.Entity<Track>().HasData(
            new Track{TrackId = 1,Name = "Silverstone Circuit",LengthInKm = (decimal)5.89},
            new Track{TrackId = 2,Name = "Monaco GP",LengthInKm = 3}
        );
        modelBuilder.Entity<Race>().HasData(
            new Race{RaceId = 1,Location = "Silverstone, UK",Date = DateTime.Parse("2025-07-14"),Name = "British Grand Prix"},
            new Race{RaceId = 2,Location = "Monaco",Date = DateTime.Parse("2025-05-14"),Name = "Monaco GP"}
            );
        
        modelBuilder.Entity<TrackRace>().HasData(
            new TrackRace{TrackRaceId = 1,RaceId = 1,TrackId = 1,BestTimeInSeconds = 5460,Laps = 52},
            new TrackRace{TrackRaceId = 2,RaceId = 2,TrackId = 2,BestTimeInSeconds = 8000,Laps = 73}
            );
        modelBuilder.Entity<RaceParticipation>().HasData(
            new RaceParticipation{TrackRaceId = 1,RacerId = 1,FinishTimeInSeconds = 5800,Position = 1});



    }
}