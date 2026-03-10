namespace Project.Scripts.UI.Core
{
    public interface IView
    {
        void Open();
        void OpenImmediately();
        void Close();
        void CloseImmediately();
    }
}