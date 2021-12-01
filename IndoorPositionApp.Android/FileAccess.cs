namespace IndoorPositionApp.Droid
{
    class FileAccess
    {
        //Direccion de almacenamiento en el dispositivo
        public static string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);

            return System.IO.Path.Combine(path, filename);
        }
    }
}