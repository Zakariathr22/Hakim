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
        [ObservableProperty] private Patient patient;        
        [ObservableProperty] private ObservableCollection<Patient> patients;
        [ObservableProperty] private int patientsOrder;
        [ObservableProperty] private Patient selectedPatient;

        public ClientsViewModel()
        {
            PatientsOrder = int.Parse(ConfigurationService.GetAppSetting("PatientsOrder"));
            Patient = new Patient();
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
                            LastName, FirstName, DateOfBirth, Gender, Address, State, City, PostalCode, 
                            Phone1, Phone1Owner, Phone2, Phone2Owner, Email, MedicalHistory, Allergies, 
                            CurrentMedications, InsuranceProvider, InsuranceNumber
                        ) VALUES (
                            @LastName, @FirstName, @DateOfBirth, @Gender, @Address, @State, @City, @PostalCode, 
                            @Phone1, @Phone1Owner, @Phone2, @Phone2Owner, @Email, @MedicalHistory, @Allergies, 
                            @CurrentMedications, @InsuranceProvider, @InsuranceNumber
                        )";

                    command.Parameters.AddWithValue("@LastName", patient.LastName);
                    command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                    command.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth.DateTime);
                    command.Parameters.AddWithValue("@Gender", patient.Gender);
                    command.Parameters.AddWithValue("@Address", patient.Address);
                    command.Parameters.AddWithValue("@State", patient.State);
                    command.Parameters.AddWithValue("@City", patient.City);
                    command.Parameters.AddWithValue("@PostalCode", patient.PostalCode);
                    command.Parameters.AddWithValue("@Phone1", patient.Phone1);
                    command.Parameters.AddWithValue("@Phone1Owner", patient.Phone1Owner);
                    command.Parameters.AddWithValue("@Phone2", patient.Phone2);
                    command.Parameters.AddWithValue("@Phone2Owner", patient.Phone2Owner);
                    command.Parameters.AddWithValue("@Email", patient.Email);
                    command.Parameters.AddWithValue("@MedicalHistory", patient.MedicalHistory);
                    command.Parameters.AddWithValue("@Allergies", patient.Allergies);
                    command.Parameters.AddWithValue("@CurrentMedications", patient.CurrentMedications);
                    command.Parameters.AddWithValue("@InsuranceProvider", patient.InsuranceProvider);
                    command.Parameters.AddWithValue("@InsuranceNumber", patient.InsuranceNumber);

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
                            Gender = Convert.ToInt32(reader["Gender"]),
                            Address = reader["Address"].ToString(),
                            State = reader["State"].ToString(),
                            City = reader["City"].ToString(),
                            PostalCode = reader["PostalCode"].ToString(),
                            Phone1 = reader["Phone1"].ToString(),
                            Phone1Owner = Convert.ToInt32(reader["Phone1Owner"]),
                            Phone2 = reader["Phone2"].ToString(),
                            Phone2Owner = Convert.ToInt32(reader["Phone2Owner"].ToString()),
                            Email = reader["Email"].ToString(),
                            MedicalHistory = reader["MedicalHistory"].ToString(),
                            Allergies = reader["Allergies"].ToString(),
                            CurrentMedications = reader["CurrentMedications"].ToString(),
                            InsuranceProvider = reader["InsuranceProvider"].ToString(),
                            InsuranceNumber = reader["InsuranceNumber"].ToString(),
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
            string orderClause = PatientsOrdrer switch
            {
                0 => "DateOfRegistration DESC",
                1 => "DateOfRegistration",
                2 => "(LastName || ' ' || FirstName)",
                3 => "(LastName || ' ' || FirstName) DESC",
                4 => "(FirstName || ' ' || LastName)",
                5 => "(FirstName || ' ' || LastName) DESC",
                6 => "DateOfBirth DESC",
                7 => "DateOfBirth",
                _ => "DateOfRegistration DESC"
            };

            return $"SELECT * FROM Patient ORDER BY {orderClause}";
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
                            State = @State,
                            City = @City,
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
                    command.Parameters.AddWithValue("@Gender", patient.Gender);
                    command.Parameters.AddWithValue("@Address", patient.Address);
                    command.Parameters.AddWithValue("@State", patient.State);
                    command.Parameters.AddWithValue("@City", patient.City);
                    command.Parameters.AddWithValue("@PostalCode", patient.PostalCode);
                    command.Parameters.AddWithValue("@Phone1", patient.Phone1);
                    command.Parameters.AddWithValue("@Phone1Owner", patient.Phone1Owner);
                    command.Parameters.AddWithValue("@Phone2", patient.Phone2);
                    command.Parameters.AddWithValue("@Phone2Owner", patient.Phone2Owner);
                    command.Parameters.AddWithValue("@Email", patient.Email);
                    command.Parameters.AddWithValue("@MedicalHistory", patient.MedicalHistory);
                    command.Parameters.AddWithValue("@Allergies", patient.Allergies);
                    command.Parameters.AddWithValue("@CurrentMedications", patient.CurrentMedications);
                    command.Parameters.AddWithValue("@InsuranceProvider", patient.InsuranceProvider);
                    command.Parameters.AddWithValue("@InsuranceNumber", patient.InsuranceNumber);
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
