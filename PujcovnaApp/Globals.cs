using DataEntity;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System;

namespace PujcovnaApp
{
    public static class Globals
    {
        // Zde používáme správný název kontextu: PujcovnaContext
        public static PujcovnaContext Context { get; set; } = null!;

        public static void UlozitData()
        {
            try
            {
                Context.SaveChanges();
                MessageBox.Show("Data byla úspěšně uložena.", "Uloženo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při ukládání: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}