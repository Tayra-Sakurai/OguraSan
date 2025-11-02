Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata

Public Class OguraEntities
    Inherits DbContext

    Public Property Table As DbSet(Of Table)


    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer(My.Settings.OguraConnectionString)
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
        modelBuilder.Entity(Of Table)()
    End Sub
End Class
