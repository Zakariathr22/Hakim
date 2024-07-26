using CommunityToolkit.Mvvm.ComponentModel;
using Hakim.Model;
using Hakim.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakim.ViewModel
{
    public partial class ClientsViewModel : ObservableObject
    {
        [ObservableProperty] private Patient newPatient = new Patient();
        [ObservableProperty] private ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

        public ClientsViewModel()
        {
            NewPatient = new Patient();
            Patients = new ObservableCollection<Patient>();
        }

        public void AddPatient(Patient patient)
        {
            try
            {
                // Ensure connection is open
                if (DataAccessService.Connection == null || DataAccessService.Connection.State != System.Data.ConnectionState.Open)
                {
                    DataAccessService.SetConnection();
                }

                using (var command = new SQLiteCommand(DataAccessService.Connection))
                {
                    command.CommandText = @"
                        INSERT INTO Patient (
                            LastName, FirstName, DateOfBirth, Gender, Address, Wilaya, Commune, PostalCode, 
                            Phone1, Phone1Owner, Phone2, Phone2Owner, Email, MedicalHistory, Allergies, 
                            CurrentMedications, InsuranceProvider, InsuranceNumber
                        ) VALUES (
                            @LastName, @FirstName, @DateOfBirth, @Gender, @Address, @Wilaya, @Commune, @PostalCode, 
                            @Phone1, @Phone1Owner, @Phone2, @Phone2Owner, @Email, @MedicalHistory, @Allergies, 
                            @CurrentMedications, @InsuranceProvider, @InsuranceNumber
                        )";

                    command.Parameters.AddWithValue("@LastName", patient.LastName);
                    command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                    command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth.DateTime);
                    command.Parameters.AddWithValue("@Gender", patient.gender);
                    command.Parameters.AddWithValue("@Address", patient.address);
                    command.Parameters.AddWithValue("@Wilaya", patient.wilaya);
                    command.Parameters.AddWithValue("@Commune", patient.commune);
                    command.Parameters.AddWithValue("@PostalCode", patient.postalCode);
                    command.Parameters.AddWithValue("@Phone1", patient.Phone1);
                    command.Parameters.AddWithValue("@Phone1Owner", patient.Phone1Owner);
                    command.Parameters.AddWithValue("@Phone2", patient.Phone2);
                    command.Parameters.AddWithValue("@Phone2Owner", patient.Phone2Owner);
                    command.Parameters.AddWithValue("@Email", patient.email);
                    command.Parameters.AddWithValue("@MedicalHistory", patient.medicalHistory);
                    command.Parameters.AddWithValue("@Allergies", patient.allergies);
                    command.Parameters.AddWithValue("@CurrentMedications", patient.currentMedications);
                    command.Parameters.AddWithValue("@InsuranceProvider", patient.insuranceProvider);
                    command.Parameters.AddWithValue("@InsuranceNumber", patient.insuranceNumber);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Patient added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the patient: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }
    }
}
