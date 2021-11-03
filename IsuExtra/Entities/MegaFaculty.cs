namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(string facultySymbol, string facultyName)
        {
            FacultySymbol = facultySymbol;
            FacultyName = facultyName;
        }

        public string FacultySymbol { get; set; }
        public string FacultyName { get; set; }
    }
}