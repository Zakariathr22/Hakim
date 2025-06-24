using System;
using System.Data.SQLite;
using System.Diagnostics;

namespace Hakim.Services;

public static class DataAccessService
{
    private static readonly string ConnectionString = $"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Hakim\\Database.sqlite";

    public static SQLiteConnection GetConnection()
    {
        var connection = new SQLiteConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    private static void ExecuteNonQuery(string commandText, string tableName)
    {
        try
        {
            using var connection = GetConnection();
            using var command = new SQLiteCommand(commandText, connection);
            command.ExecuteNonQuery();
            Debug.WriteLine($"{tableName} table ensured to exist.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred while ensuring the {tableName} table: {ex.Message}");
        }
    }

    public static void SetupDatabaseSchema()
    {
        ExecuteNonQuery(@"
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
                )", "Patient");

        ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS File (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    patient_id INTEGER,
                    title TEXT,
                    creation_date DATETIME,
                    url TEXT,
                    type INTEGER,
                    FOREIGN KEY (patient_id) REFERENCES Patient (id) ON DELETE CASCADE
                )", "File");

        ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS Appointment (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    patient_id INTEGER,
                    AppointmentDate DATE,
                    AppointmentHour TIME,
                    Purpose TEXT,
                    Notes TEXT,
                    FOREIGN KEY (patient_id) REFERENCES Patient (id) ON DELETE CASCADE
                )", "Appointment");

        ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS MedicalConsultation (
                    file_id INTEGER PRIMARY KEY,
                    notes TEXT,
                    prescription TEXT,
                    FOREIGN KEY (file_id) REFERENCES File (id) ON DELETE CASCADE
                )", "MedicalConsultation");

        ExecuteNonQuery(@"
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
                )", "SurgeryProtocol");

        ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS XRay (
                    file_id INTEGER PRIMARY KEY,
                    xray_date DATETIME,
                    xray_time TIME,
                    radiologist TEXT,
                    diagnosis TEXT,
                    type INTEGER,
                    FOREIGN KEY (file_id) REFERENCES File (id) ON DELETE CASCADE
                )", "XRay");

        ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS BackSpineTelemetryXRay (
                    xray_id INTEGER PRIMARY KEY,
                    vls INTEGER,
                    vli INTEGER,
                    cobb INTEGER,
                    bend INTEGER,
                    red INTEGER,
                    FOREIGN KEY (xray_id) REFERENCES XRay (file_id) ON DELETE CASCADE
                )", "BackSpineTelemetryXRay");

        ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS Fee (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    patient_id INTEGER,
                    visit_date DATETIME,
                    amount DECIMAL(10, 2),
                    paid_amount DECIMAL(10, 2),
                    notes TEXT,
                    FOREIGN KEY (patient_id) REFERENCES Patient(id) ON DELETE CASCADE
                )", "Fee");
    }
}
