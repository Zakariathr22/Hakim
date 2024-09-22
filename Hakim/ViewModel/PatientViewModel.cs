using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class PatientViewModel : ObservableObject
    {
        [ObservableProperty] private Patient selectedPatient;
        [ObservableProperty] private MedicalConsultation consultation;
        [ObservableProperty] private XRay xRay;
        [ObservableProperty] private SpineTelemetryXRay spineTelemetryXRay;
        [ObservableProperty] private SurgeryProtocol surgeryProtocol;
        [ObservableProperty] private Dictionary<DateTime, int> appointmentCounts;
        [ObservableProperty] private Appointment appointment;
        [ObservableProperty] private int filesOrder;
        [ObservableProperty] private int filesFilter;
        [ObservableProperty] private int appointmentOrder;

        public PatientViewModel()
        {
            FilesOrder = 0;
            FilesFilter = 0;
            AppointmentOrder = 1;
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
        }

        private ObservableCollection<File> GetFilesByPatient(Patient patient)
        {
            var files = new ObservableCollection<File>();

            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(ReturnFilesSelectionQuery(FilesOrder, FilesFilter), connection))
                {
                    command.Parameters.AddWithValue("@PatientId", patient.id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var file = new File
                            {
                                id = Convert.ToInt32(reader["id"]),
                                Patient = patient,
                                Title = reader["title"].ToString(),
                                CreationDate = Convert.ToDateTime(reader["creation_date"]),
                                Url = reader["url"].ToString(),
                                Type = Convert.ToInt32(reader["type"])
                            };

                            files.Add(file);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving files for patient {patient.FirstName} {patient.LastName}: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }

            return files;
        }

        [RelayCommand]
        private void getFilesByPatient()
        {
            SelectedPatient.files = GetFilesByPatient(SelectedPatient);
        }

        private ObservableCollection<Appointment> GetAppointmentsByPatient(Patient patient)
        {
            var appointments = new ObservableCollection<Appointment>();

            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(ReturnAppointmentSelectionQuery(AppointmentOrder), connection))
                {
                    command.Parameters.AddWithValue("@PatientId", patient.id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var appointment = new Appointment
                            {
                                id = Convert.ToInt32(reader["id"]),
                                Patient = patient,
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                AppointmentTime = TimeSpan.Parse(reader.GetString(reader.GetOrdinal("AppointmentHour"))),
                                Purpose = reader["Purpose"].ToString(),
                                Notes = reader["Notes"].ToString()
                            };

                            appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving appointments for patient {patient.FirstName} {patient.LastName}: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }

            return appointments;
        }

        [RelayCommand]
        private void getAppointmentsByPatient()
        {
            SelectedPatient.appointments = GetAppointmentsByPatient(SelectedPatient);
        }

        public void AddMedicalConsultation()
        {
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    // Insert into the File table
                    command.CommandText = @"
                        INSERT INTO File (
                            patient_id, title, creation_date, type
                        ) VALUES (
                            @PatientId, @Title, @CreationDate, @Type
                        );
                        SELECT last_insert_rowid();";  // Retrieve the last inserted ID
                    command.Parameters.AddWithValue("@PatientId", Consultation.Patient.id);
                    command.Parameters.AddWithValue("@Title", Consultation.Title);
                    command.Parameters.AddWithValue("@CreationDate", Consultation.CreationDate);
                    command.Parameters.AddWithValue("@Type", 0);

                    // Execute the command and get the last inserted file_id
                    Consultation.id = Convert.ToInt32(command.ExecuteScalar());

                    // Insert into the MedicalConsultation table
                    command.CommandText = @"
                        INSERT INTO MedicalConsultation (
                            file_id, notes, prescription
                        ) VALUES (
                            @FileId, @Notes, @Prescription
                        )";
                    command.Parameters.AddWithValue("@FileId", Consultation.id);
                    command.Parameters.AddWithValue("@Notes", Consultation.Notes);
                    command.Parameters.AddWithValue("@Prescription", Consultation.Prescription);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Medical consultation added successfully.");
                    SelectedPatient.files = GetFilesByPatient(SelectedPatient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the medical consultation: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        public void AddXRay()
        {
            FileManagementService.CreateNewFolder(@"D:\", "Hakim");
            FileManagementService.CreateNewFolder(@"D:\Hakim\", SelectedPatient.id.ToString());
            string newURL = FileManagementService.CopyFileToFolder(
                XRay.Url,
                @"D:\Hakim\" + SelectedPatient.id.ToString() + @"\",
                $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}-XRay-{SelectedPatient.fullName}");
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    // Insert into the File table
                    command.CommandText = @"
                        INSERT INTO File (
                            patient_id, title, creation_date, type, url
                        ) VALUES (
                            @PatientId, @Title, @CreationDate, @Type, @Url
                        );
                        SELECT last_insert_rowid();";  // Retrieve the last inserted ID
                    command.Parameters.AddWithValue("@PatientId", XRay.Patient.id);
                    command.Parameters.AddWithValue("@Title", XRay.Title);
                    command.Parameters.AddWithValue("@CreationDate", XRay.CreationDate);
                    command.Parameters.AddWithValue("@Type", 1);  // Automatically set type to "Radiographie"
                    command.Parameters.AddWithValue("@Url", newURL);

                    // Execute the command and get the last inserted file_id
                    XRay.id = Convert.ToInt32(command.ExecuteScalar());

                    // Insert into the XRay table
                    command.CommandText = @"
                        INSERT INTO XRay (
                            file_id, xray_date, xray_time, radiologist, diagnosis, type
                        ) VALUES (
                            @FileId, @XrayDate, @XrayTime, @Radiologist, @Diagnosis, @XrayType
                        )";
                    command.Parameters.AddWithValue("@FileId", XRay.id);
                    command.Parameters.AddWithValue("@XrayDate", XRay.Xray_date);
                    command.Parameters.AddWithValue("@XrayTime", XRay.XrayTime);
                    command.Parameters.AddWithValue("@Url", XRay.Url);
                    command.Parameters.AddWithValue("@Radiologist", XRay.Radiologist);
                    command.Parameters.AddWithValue("@Diagnosis", XRay.Diagnosis);
                    command.Parameters.AddWithValue("@XrayType", "Radiographie");  // Set type as "Radiographie"

                    command.ExecuteNonQuery();
                    Console.WriteLine("X-Ray added successfully.");
                    SelectedPatient.files = GetFilesByPatient(SelectedPatient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the X-Ray: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        public void AddSpineTelemetryXRay()
        {
            FileManagementService.CreateNewFolder(@"D:\", "Hakim");
            FileManagementService.CreateNewFolder(@"D:\Hakim\", SelectedPatient.id.ToString());
            string newURL = FileManagementService.CopyFileToFolder(
                SpineTelemetryXRay.Url,
                @"D:\Hakim\" + SelectedPatient.id.ToString() + @"\",
                $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}-SpineTelemetryXRay-{SelectedPatient.fullName}");
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    // Insert into the File table
                    command.CommandText = @"
                        INSERT INTO File (
                            patient_id, title, creation_date, type, url
                        ) VALUES (
                            @PatientId, @Title, @CreationDate, @Type, @Url
                        );
                        SELECT last_insert_rowid();";  // Retrieve the last inserted ID
                    command.Parameters.AddWithValue("@PatientId", SpineTelemetryXRay.Patient.id);
                    command.Parameters.AddWithValue("@Title", SpineTelemetryXRay.Title);
                    command.Parameters.AddWithValue("@CreationDate", SpineTelemetryXRay.CreationDate);
                    command.Parameters.AddWithValue("@Type", 2);  // Automatically set type to "Radiographie"
                    command.Parameters.AddWithValue("@Url", newURL);

                    // Execute the command and get the last inserted file_id
                    SpineTelemetryXRay.id = Convert.ToInt32(command.ExecuteScalar());

                    // Insert into the XRay table
                    command.CommandText = @"
                        INSERT INTO XRay (
                            file_id, xray_date, xray_time, radiologist, diagnosis, type
                        ) VALUES (
                            @FileId, @XrayDate, @XrayTime, @Radiologist, @Diagnosis, @XrayType
                        )";
                    command.Parameters.AddWithValue("@FileId", SpineTelemetryXRay.id);
                    command.Parameters.AddWithValue("@XrayDate", SpineTelemetryXRay.Xray_date);
                    command.Parameters.AddWithValue("@XrayTime", SpineTelemetryXRay.XrayTime);
                    command.Parameters.AddWithValue("@Radiologist", SpineTelemetryXRay.Radiologist);
                    command.Parameters.AddWithValue("@Diagnosis", SpineTelemetryXRay.Diagnosis);
                    command.Parameters.AddWithValue("@XrayType", "Spine Telemetry");  // Set type as "Spine Telemetry"

                    command.ExecuteNonQuery();

                    // Insert into the BackXSpineTelemetryXRay table
                    command.CommandText = @"
                        INSERT INTO BackXSpineTelemetryXRay (
                            xray_id, vls, vli, cobb, bend, red
                        ) VALUES (
                            @XrayId, @Vls, @Vli, @Cobb, @Bend, @Red
                        )";
                    command.Parameters.AddWithValue("@XrayId", SpineTelemetryXRay.id);
                    command.Parameters.AddWithValue("@Vls", SpineTelemetryXRay.VLS);
                    command.Parameters.AddWithValue("@Vli", SpineTelemetryXRay.VLI);
                    command.Parameters.AddWithValue("@Cobb", SpineTelemetryXRay.COBB);
                    command.Parameters.AddWithValue("@Bend", SpineTelemetryXRay.BEND);
                    command.Parameters.AddWithValue("@Red", SpineTelemetryXRay.RED);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Spine Telemetry X-Ray added successfully.");
                    SelectedPatient.files = GetFilesByPatient(SelectedPatient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the Spine Telemetry X-Ray: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        public void AddSurgeryProtocol()
        {
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    // Insert into the File table
                    command.CommandText = @"
                        INSERT INTO File (
                            patient_id, title, creation_date, type
                        ) VALUES (
                            @PatientId, @Title, @CreationDate, @Type
                        );
                        SELECT last_insert_rowid();";  // Retrieve the last inserted ID
                    command.Parameters.AddWithValue("@PatientId", SurgeryProtocol.Patient.id);
                    command.Parameters.AddWithValue("@Title", SurgeryProtocol.Title);
                    command.Parameters.AddWithValue("@CreationDate", SurgeryProtocol.CreationDate);
                    command.Parameters.AddWithValue("@Type", 3);  // Set type to "Surgery Protocol"

                    // Execute the command and get the last inserted file_id
                    SurgeryProtocol.id = Convert.ToInt32(command.ExecuteScalar());

                    // Insert into the SurgeryProtocol table
                    command.CommandText = @"
                        INSERT INTO SurgeryProtocol (
                            file_id, surgeon, operating_assistant, instrument_technician, anesthetist,
                            scrub_nurse, intervention_date, intervention_time, diagnosis, intervention, operative_report
                        ) VALUES (
                            @FileId, @Surgeon, @OperatingAssistant, @InstrumentTechnician, @Anesthetist,
                            @ScrubNurse, @InterventionDate, @InterventionTime, @Diagnosis, @Intervention, @OperativeReport
                        )";
                    command.Parameters.AddWithValue("@FileId", SurgeryProtocol.id);
                    command.Parameters.AddWithValue("@Surgeon", SurgeryProtocol.Surgeon);
                    command.Parameters.AddWithValue("@OperatingAssistant", SurgeryProtocol.Operating_assistant);
                    command.Parameters.AddWithValue("@InstrumentTechnician", SurgeryProtocol.Instrument_technician);
                    command.Parameters.AddWithValue("@Anesthetist", SurgeryProtocol.Anesthetist);
                    command.Parameters.AddWithValue("@ScrubNurse", SurgeryProtocol.Scrub_nurse);
                    command.Parameters.AddWithValue("@InterventionDate", SurgeryProtocol.Intervention_date);
                    command.Parameters.AddWithValue("@InterventionTime", SurgeryProtocol.Intervention_time);
                    command.Parameters.AddWithValue("@Diagnosis", SurgeryProtocol.Diagnosis);
                    command.Parameters.AddWithValue("@Intervention", SurgeryProtocol.Intervention);
                    command.Parameters.AddWithValue("@OperativeReport", SurgeryProtocol.Operative_report);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Surgery protocol added successfully.");
                    SelectedPatient.files = GetFilesByPatient(SelectedPatient);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the surgery protocol: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private Dictionary<DateTime, int> GetNumberOfAppointmentsPerDay()
        {
            var appointmentCounts = new Dictionary<DateTime, int>();

            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand("SELECT DATE(AppointmentDate) AS AppointmentDay, COUNT(*) AS AppointmentCount FROM Appointment GROUP BY AppointmentDay ORDER BY AppointmentDay;", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var appointmentDate = Convert.ToDateTime(reader["AppointmentDay"]).Date;
                        var count = Convert.ToInt32(reader["AppointmentCount"]);

                        // Add or update the dictionary with the count for the given date
                        appointmentCounts[appointmentDate] = count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving appointment counts: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }

            return appointmentCounts;
        }

        [RelayCommand]
        private void getNumberOfAppointmentsPerDay()
        {
            AppointmentCounts = GetNumberOfAppointmentsPerDay();
        }

        public void AddAppointment()
        {
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    // Insert into the Appointment table
                    command.CommandText = @"
                INSERT INTO Appointment (
                    patient_id, AppointmentDate, AppointmentHour, Purpose, Notes
                ) VALUES (
                    @PatientId, @AppointmentDate, @AppointmentHour, @Purpose, @Notes
                );";  // Retrieve the last inserted ID

                    command.Parameters.AddWithValue("@PatientId", Appointment.Patient.id);
                    command.Parameters.AddWithValue("@AppointmentDate", Appointment.AppointmentDate.Date);
                    command.Parameters.AddWithValue("@AppointmentHour", Appointment.AppointmentTime);
                    command.Parameters.AddWithValue("@Purpose", Appointment.Purpose);
                    command.Parameters.AddWithValue("@Notes", Appointment.Notes);

                    // Execute the command and get the last inserted appointment id
                    command.ExecuteNonQuery();

                    Console.WriteLine("Appointment added successfully.");

                    SelectedPatient.appointments = GetAppointmentsByPatient(SelectedPatient);
                    AppointmentCounts = GetNumberOfAppointmentsPerDay();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the appointment: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private string ReturnFilesSelectionQuery(int order, int filter)
        {
            string orderClause = order switch
            {
                0 => "creation_date DESC",
                1 => "creation_date",
                2 => "Title",
                3 => "Title DESC",
                4 => "Type",
                5 => "Type DESC",
                _ => "creation_date DESC"
            };

            string filterClause = filter switch
            {
                1 => "WHERE Type = 0 AND patient_id = @PatientId",
                2 => "WHERE Type = 1 AND patient_id = @PatientId",
                3 => "WHERE Type = 2 AND patient_id = @PatientId",
                4 => "WHERE Type = 3 AND patient_id = @PatientId",
                _ => "WHERE patient_id = @PatientId"  // 0 for AllFiles
            };

            return $"SELECT * FROM File {filterClause} ORDER BY {orderClause}";
        }

        [RelayCommand]
        private void FilesOrderChanged()
        {
            ConfigurationService.SetAppSetting("FilesOrder", FilesOrder);
            SelectedPatient.files = GetFilesByPatient(SelectedPatient);
        }

        [RelayCommand]
        private void FilesFilterChanged()
        {
            ConfigurationService.SetAppSetting("FilesFilter", FilesFilter);
            SelectedPatient.files = GetFilesByPatient(SelectedPatient);
        }

        public void DeleteFileById(int patientId)
        {
            try
            {
                using (var connection = DataAccessService.GetConnection())
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        PRAGMA foreign_keys = ON;
                        DELETE FROM File WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", patientId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("File deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No file found with the provided ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }

            SelectedPatient.files = GetFilesByPatient(SelectedPatient);
        }

        private string ReturnAppointmentSelectionQuery(int order)
        {
            string orderClause = order switch
            {
                0 => "AppointmentDate ASC, AppointmentHour ASC",
                1 => "AppointmentDate DESC, AppointmentHour DESC",
                _ => "AppointmentDate ASC, AppointmentHour ASC"
            };

            return $"SELECT * FROM Appointment WHERE patient_id = @PatientId ORDER BY {orderClause}";
        }
    }
}
