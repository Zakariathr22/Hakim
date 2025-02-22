using System;
using System.Data.SQLite;
using System.Diagnostics;

namespace Hakim.Service
{
    public static class DataAccessService
    {
        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(
                $"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Hakim\\Database.sqlite");
            connection.Open();
            return connection;
        }

        private static void EnsurePatientTableExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Patient (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        LastName TEXT,
                        FirstName TEXT,
                        DateOfBirth DATE,
                        Gender INTEGER,
                        Address TEXT,
                        State TEXT,
                        City TEXT,
                        PostalCode TEXT,
                        Phone1 TEXT,
                        Phone1Owner INTEGER,
                        Phone2 TEXT,
                        Phone2Owner INTEGER,
                        Email TEXT,
                        MedicalHistory TEXT,
                        Allergies TEXT,
                        CurrentMedications TEXT,
                        InsuranceProvider TEXT,
                        InsuranceNumber TEXT,
                        DateOfRegistration DATETIME DEFAULT CURRENT_TIMESTAMP
                    )";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("Patient table ensured to exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while ensuring the Patient table: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private static void EnsureFileTableExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS File (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        patient_id INTEGER,
                        title TEXT,
                        creation_date DATETIME,
                        url TEXT,
                        type INTEGER,
                        FOREIGN KEY (patient_id) REFERENCES Patient (id) ON DELETE CASCADE
                    );";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("File table ensured to exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while ensuring the File table: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private static void EnsureAppointmentTableExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Appointment (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        patient_id INTEGER,
                        AppointmentDate DATE,
                        AppointmentHour TIME,
                        Purpose TEXT,
                        Notes TEXT,
                        FOREIGN KEY (patient_id) REFERENCES Patient (id) ON DELETE CASCADE
                    );";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("Appointment table ensured to exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while ensuring the Appointment table: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private static void EnsureMedicalConsultationTableExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS MedicalConsultation (
                        file_id INTEGER PRIMARY KEY,
                        notes TEXT,
                        prescription TEXT,
                        FOREIGN KEY (file_id) REFERENCES File (id) ON DELETE CASCADE
                    );";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("Medical Consultation table ensured to exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while ensuring the Medical Consultation table: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private static void EnsureSurgeryProtocolTableExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS SurgeryProtocol (
                        file_id INTEGER PRIMARY KEY,
                        surgeon TEXT,
                        operating_assistant TEXT,
                        instrument_technician TEXT,
                        anesthetist TEXT,
                        scrub_nurse TEXT,
                        intervention_date DATE,
                        intervention_time TIME,
                        diagnosis TEXT,
                        intervention TEXT,
                        operative_report TEXT,
                        FOREIGN KEY (file_id) REFERENCES File (id) ON DELETE CASCADE
                    );";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("Surgery Protocol table ensured to exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while ensuring the Surgery Protocol table: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private static void EnsureXRayTableExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS XRay (
                        file_id INTEGER PRIMARY KEY,
                        xray_date DATETIME,
                        xray_time TIME,
                        radiologist TEXT,
                        diagnosis TEXT,
                        type INTEGER,
                        FOREIGN KEY (file_id) REFERENCES File (id) ON DELETE CASCADE
                    );";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("X-ray table ensured to exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while ensuring the X-ray table: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private static void EnsureBackSpineTelemetryXRayTableExists()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS BackSpineTelemetryXRay (
                        xray_id INTEGER PRIMARY KEY,
                        vls INTEGER,
                        vli INTEGER,
                        cobb INTEGER,
                        bend INTEGER,
                        red INTEGER,
                        FOREIGN KEY (xray_id) REFERENCES XRay (file_id) ON DELETE CASCADE
                    );";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("Back Spine Telemetry X-Ray table ensured to exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while ensuring the Back Spine Telemetry X-Ray table: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        private static void AddSampleData()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    // Check if the Patient table exists and create it if it doesn't
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = @"
                    INSERT INTO
    Patient (
        LastName,
        FirstName,
        DateOfBirth,
        Gender,
        Address,
        State,
        City,
        PostalCode,
        Phone1,
        Phone1Owner,
        Phone2,
        Phone2Owner,
        Email,
        MedicalHistory,
        Allergies,
        CurrentMedications,
        InsuranceProvider,
        InsuranceNumber
    )
VALUES
    -- Patient 1
    (
        ""Benamar"",
        ""Nadir"",
        ""1985-04-12"",
        0,
        ""Rue des Martyrs, N°15"",
        ""Alger"",
        ""Bab El Oued"",
        ""16000"",
        ""0551234567"",
        0,
        ""0772345678"",
        0,
        ""nadir.benamar@example.com"",
        ""Antécédents de fracture du fémur droit en 2018. Douleurs lombaires chroniques. Intervention chirurgicale de l'épaule en 2020."",
        ""Aucun"",
        ""Paracétamol 500mg"",
        ""CNAS"",
        ""123456789""
    ),
    -- Patient 2
    (
        ""Mehdi"",
        ""Khalida"",
        ""1990-09-23"",
        1,
        ""Avenue de l'Indépendance, N°22"",
        ""Oran"",
        ""El M'Naouer"",
        ""31000"",
        ""0698765432"",
        0,
        ""0659876543"",
        0,
        ""khalida.mehdi@example.com"",
        ""Arthrose du genou droit depuis 2015. Hernie discale opérée en 2019. Problèmes de dos fréquents."",
        ""Ibuprofène"",
        ""Aspirine"",
        ""CASNOS"",
        ""987654321""
    ),
    -- Patient 3
    (
        ""Belkacem"",
        ""Amin"",
        ""1978-11-06"",
        0,
        ""Boulevard de la République, N°5"",
        ""Constantine"",
        ""Ziadia"",
        ""25000"",
        ""0777654321"",
        0,
        ""0798765432"",
        1,
        ""amin.belkacem@example.com"",
        ""Antécédents de scoliose opérée en 2000. Douleurs articulaires aux mains. Prothèse de hanche gauche en 2022."",
        ""Gluten"",
        ""Ibuprofène"",
        ""CNAS"",
        ""456789123""
    ),
    -- Patient 4
    (
        ""Cherif"",
        ""Lamia"",
        ""1987-02-19"",
        1,
        ""Rue de la Liberté, N°3"",
        ""Tlemcen"",
        ""Mansourah"",
        ""13000"",
        ""0557654321"",
        0,
        ""0668765432"",
        0,
        ""lamia.cherif@example.com"",
        ""Fracture du poignet gauche en 2019. Tendinite du genou droit. Douleurs chroniques au bas du dos."",
        ""Aucun"",
        ""Diclofénac"",
        ""CASNOS"",
        ""654321987""
    ),
    -- Patient 5
    (
        ""Zouaoui"",
        ""Rachid"",
        ""1982-08-14"",
        0,
        ""Cité des Palmiers, N°18"",
        ""Annaba"",
        ""El Bouni"",
        ""23000"",
        ""0655432198"",
        0,
        ""0791234567"",
        1,
        ""rachid.zouaoui@example.com"",
        ""Antécédents de polyarthrite rhumatoïde. Douleurs fréquentes aux genoux et aux hanches. Rééducation après blessure sportive en 2021."",
        ""Lactose"",
        ""Corticoïdes"",
        ""CNAS"",
        ""321654987""
    );

INSERT INTO
    File (
        patient_id,
        title,
        creation_date,
        url,
        type
    )
VALUES
    -- File 1: Medical Consultation
    (
        1,
        ""Consultation Médicale - Nadir Benamar"",
        ""2024-08-15 09:30:00"",
        ""url_vers_dossier_1"",
        0
    ),
    -- File 2: X-Ray
    (
        2,
        ""Radiographie du Genou - Khalida Mehdi"",
        ""2024-08-20 10:00:00"",
        ""url_vers_dossier_2"",
        1
    ),
    -- File 3: Back X-Spine Telemetry X-Ray
    (
        3,
        ""Radiographie Téléométrique du Rachis - Amin Belkacem"",
        ""2024-08-25 11:00:00"",
        ""url_vers_dossier_3"",
        2
    ),
    -- File 4: Surgery Protocol
    (
        4,
        ""Protocole Chirurgical - Lamia Cherif"",
        ""2024-08-30 14:00:00"",
        ""url_vers_dossier_4"",
        3
    ),
    -- File 5: Medical Consultation
    (
        5,
        ""Consultation Médicale - Rachid Zouaoui"",
        ""2024-09-01 15:30:00"",
        ""url_vers_dossier_5"",
        0
    );

INSERT INTO
    File (
        patient_id,
        title,
        creation_date,
        url,
        type
    )
VALUES
    -- File 6: X-Ray
    (
        1,
        ""Radiographie de la Hanche - Nadir Benamar"",
        ""2024-07-12 09:00:00"",
        ""url_vers_dossier_6"",
        1
    ),
    -- File 7: Surgery Protocol
    (
        2,
        ""Protocole Chirurgical - Khalida Mehdi"",
        ""2024-06-10 08:30:00"",
        ""url_vers_dossier_7"",
        3
    ),
    -- File 8: Medical Consultation
    (
        3,
        ""Consultation Médicale - Amin Belkacem"",
        ""2024-05-20 16:00:00"",
        ""url_vers_dossier_8"",
        0
    ),
    -- File 9: Back X-Spine Telemetry X-Ray
    (
        4,
        ""Radiographie du Rachis - Lamia Cherif"",
        ""2024-04-25 10:15:00"",
        ""url_vers_dossier_9"",
        2
    ),
    -- File 10: X-Ray
    (
        5,
        ""Radiographie du Genou - Rachid Zouaoui"",
        ""2024-03-18 11:45:00"",
        ""url_vers_dossier_10"",
        1
    ),
    -- File 11: Medical Consultation
    (
        1,
        ""Consultation Médicale - Nadir Benamar"",
        ""2024-02-15 14:30:00"",
        ""url_vers_dossier_11"",
        0
    ),
    -- File 12: Surgery Protocol
    (
        3,
        ""Protocole Chirurgical - Amin Belkacem"",
        ""2024-01-22 13:00:00"",
        ""url_vers_dossier_12"",
        3
    ),
    -- File 13: Back X-Spine Telemetry X-Ray
    (
        2,
        ""Radiographie Téléométrique du Rachis - Khalida Mehdi"",
        ""2024-08-21 09:45:00"",
        ""url_vers_dossier_13"",
        2
    ),
    -- File 14: X-Ray
    (
        4,
        ""Radiographie de l'Épaule - Lamia Cherif"",
        ""2024-09-02 15:00:00"",
        ""url_vers_dossier_14"",
        1
    ),
    -- File 15: Medical Consultation
    (
        5,
        ""Consultation Médicale - Rachid Zouaoui"",
        ""2024-09-05 12:00:00"",
        ""url_vers_dossier_15"",
        0
    );

INSERT INTO
    Appointment (
        patient_id,
        AppointmentDate,
        AppointmentHour,
        Purpose,
        Notes
    )
VALUES
    -- Appointment 1
    (
        1,
        ""2024-08-20"",
        ""09:30:00"",
        ""Consultation de suivi"",
        ""Examen de routine pour évaluer l'état post-opératoire.""
    ),
    -- Appointment 2
    (
        2,
        ""2024-08-22"",
        ""10:00:00"",
        ""Radiographie du genou"",
        ""Préparation pour une éventuelle intervention chirurgicale.""
    ),
    -- Appointment 3
    (
        3,
        ""2024-08-25"",
        ""11:15:00"",
        ""Consultation pour douleur lombaire"",
        ""Douleur persistante malgré le traitement.""
    ),
    -- Appointment 4
    (
        4,
        ""2024-08-28"",
        ""14:00:00"",
        ""Réévaluation après chirurgie"",
        ""Suivi après la pose de prothèse.""
    ),
    -- Appointment 5
    (
        5,
        ""2024-09-01"",
        ""15:45:00"",
        ""Consultation pour douleur articulaire"",
        ""Évaluation de l'efficacité du traitement.""
    ),
    -- Appointment 6
    (
        1,
        ""2024-09-05"",
        ""09:00:00"",
        ""Radiographie de la hanche"",
        ""Contrôle post-opératoire.""
    ),
    -- Appointment 7
    (
        2,
        ""2024-09-08"",
        ""10:30:00"",
        ""Consultation pour arthrose"",
        ""Discussion sur les options de traitement.""
    ),
    -- Appointment 8
    (
        3,
        ""2024-09-12"",
        ""16:00:00"",
        ""Consultation de suivi"",
        ""Amélioration des symptômes.""
    ),
    -- Appointment 9
    (
        4,
        ""2024-09-15"",
        ""10:15:00"",
        ""Radiographie du rachis"",
        ""Analyse de la progression de la scoliose.""
    ),
    -- Appointment 10
    (
        5,
        ""2024-09-20"",
        ""11:30:00"",
        ""Consultation pour douleur au genou"",
        ""Douleur persistante nécessitant une nouvelle évaluation.""
    );";

                        command.ExecuteNonQuery();
                        Debug.WriteLine("Sample Data Added");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while Adding Sample Data: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        public static void SetupDatabaseSchema()
        {
            EnsurePatientTableExists();
            EnsureFileTableExists();
            EnsureAppointmentTableExists();
            EnsureMedicalConsultationTableExists();
            EnsureSurgeryProtocolTableExists();
            EnsureXRayTableExists();
            EnsureBackSpineTelemetryXRayTableExists();

            //AddSampleData();
        }


    }
}
