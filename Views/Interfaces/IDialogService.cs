namespace BookingCalc.Views.Interfaces
{
    public interface IDialogService
    {
        bool? Confirm(String title, String message);
    }
}
