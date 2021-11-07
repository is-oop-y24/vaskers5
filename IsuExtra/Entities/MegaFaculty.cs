namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(char facultySymbol, string facultyName)
        {
            FacultySymbol = facultySymbol;
            FacultyName = facultyName;
        }

        public char FacultySymbol { get; set; }
        public string FacultyName { get; set; }
    }
}