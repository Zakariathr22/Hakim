using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hakim.Model;
using Hakim.Service;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace Hakim.ViewModel
{
    public partial class ClientsViewModel : ObservableObject
    {
        [ObservableProperty] private Patient newPatient;
        [ObservableProperty] private ObservableCollection<Patient> patients;
        [ObservableProperty] private int patientsOrder;

        public ClientsViewModel()
        {
            PatientsOrder = int.Parse(ConfigurationService.GetAppSetting("PatientsOrder"));
            NewPatient = new Patient();
            Patients = GetAllPatients();
        }

        public void AddPatient(Patient patient)
        {
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
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

            Patients = GetAllPatients();
        }

        private ObservableCollection<Patient> GetAllPatients()
        {
            var patients = new ObservableCollection<Patient>();

            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(ReturnQuery(PatientsOrder), connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var patient = new Patient
                        {
                            id = Convert.ToInt32(reader["id"]),
                            LastName = reader["LastName"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            gender = reader["Gender"].ToString(),
                            address = reader["Address"].ToString(),
                            wilaya = reader["Wilaya"].ToString(),
                            commune = reader["Commune"].ToString(),
                            postalCode = reader["PostalCode"].ToString(),
                            Phone1 = reader["Phone1"].ToString(),
                            Phone1Owner = reader["Phone1Owner"].ToString(),
                            Phone2 = reader["Phone2"].ToString(),
                            Phone2Owner = reader["Phone2Owner"].ToString(),
                            email = reader["Email"].ToString(),
                            medicalHistory = reader["MedicalHistory"].ToString(),
                            allergies = reader["Allergies"].ToString(),
                            currentMedications = reader["CurrentMedications"].ToString(),
                            insuranceProvider = reader["InsuranceProvider"].ToString(),
                            insuranceNumber = reader["InsuranceNumber"].ToString(),
                            dateOfRegistration = Convert.ToDateTime(reader["DateOfRegistration"])
                        };

                        patients.Add(patient);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving patients: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }

            return patients;
        }

        [RelayCommand]
        private void getAllPatients()
        {
            Patients = GetAllPatients();
        }

        private string ReturnQuery(int PatientsOrdrer)
        {
            if (PatientsOrdrer == 0)
                return "SELECT * FROM Patient ORDER BY DateOfRegistration DESC";
            else if(PatientsOrdrer == 1) 
                return "SELECT * FROM Patient ORDER BY DateOfRegistration";
            else if (PatientsOrdrer == 2)
                return "SELECT * FROM Patient ORDER BY LastName";
            else if (PatientsOrdrer == 3)
                return "SELECT * FROM Patient ORDER BY LastName DESC";
            else if (PatientsOrdrer == 4)
                return "SELECT * FROM Patient ORDER BY FirstName";
            else if (PatientsOrdrer == 5)
                return "SELECT * FROM Patient ORDER BY FirstName DESC";
            else if (PatientsOrdrer == 6)
                return "SELECT * FROM Patient ORDER BY DateOfBirth DESC";
            else if (PatientsOrdrer == 7)
                return "SELECT * FROM Patient ORDER BY DateOfBirth";
            else 
                return "SELECT * FROM Patient ORDER BY DateOfRegistration DESC";
        }

        [RelayCommand]
        private void OrderChanged()
        {
            ConfigurationService.SetAppSetting("PatientsOrder", PatientsOrder);
            Patients = GetAllPatients();
        }

        public void DeletePatientById(int patientId)
        {
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                DELETE FROM Patient 
                WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", patientId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Patient deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No patient found with the provided ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the patient: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }

            Patients = GetAllPatients();
        }

        [RelayCommand]
        public void DeletePatient(Patient patient)
        {
            if (patient == null) return;

            // Delete from database
            DeletePatientById(patient.id);

            // Remove from ObservableCollection
            Patients.Remove(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                UPDATE Patient SET
                    LastName = @LastName,
                    FirstName = @FirstName,
                    DateOfBirth = @DateOfBirth,
                    Gender = @Gender,
                    Address = @Address,
                    Wilaya = @Wilaya,
                    Commune = @Commune,
                    PostalCode = @PostalCode,
                    Phone1 = @Phone1,
                    Phone1Owner = @Phone1Owner,
                    Phone2 = @Phone2,
                    Phone2Owner = @Phone2Owner,
                    Email = @Email,
                    MedicalHistory = @MedicalHistory,
                    Allergies = @Allergies,
                    CurrentMedications = @CurrentMedications,
                    InsuranceProvider = @InsuranceProvider,
                    InsuranceNumber = @InsuranceNumber
                WHERE id = @id";

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
                    command.Parameters.AddWithValue("@id", patient.id);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Patient updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the patient: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }

            Patients = GetAllPatients();
        }
    }
}
