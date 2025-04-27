namespace Hakim.Models;

public partial class MedicalConsultation : File
{
    private string notes;
    private string prescription;
}
public partial class MedicalConsultation 
{
    public string Notes
    {
        get => notes;
        set
        {
            notes = value;
            OnPropertyChanged();
        }
    }
    public string Prescription
    {
        get => prescription;
        set
        {
            prescription = value;
            OnPropertyChanged();
        }
    }
}
