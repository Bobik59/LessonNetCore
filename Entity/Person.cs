namespace LessonNetCore.Entity
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime DateOfBirth { get; }
        public string OtherInfo { get; }

        public Person(string firstName, string lastName, DateTime dateOfBirth, string otherInfo = "")
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            OtherInfo = otherInfo;
        }

        public override string ToString()
        {
            return $"Имя: {FirstName}\nФамилия: {LastName}\nДата рождения: {DateOfBirth:dd.MM.yyyy}\nДоп. информация: {OtherInfo}";
        }
    }

}
