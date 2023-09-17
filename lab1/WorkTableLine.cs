namespace lab1;

public class WorkTableLine
{
    public WorkTime WorkTime { get; set; } = null!;
    public string Work { get; set; } = null!;

    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    public int Time { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    public int PCHP { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    public int PCOP { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    public int ПCHP { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    public int ПCOP { get; set; }
    public int P { get; set; }

    public override string ToString()
    {
        var space = ' ';
        
        return $"|{new string(space, 6 - Work.Length)}{Work}" +
               $"|{new string(space, 5 - Time.ToString().Length)}{Time}" +
               $"|{new string(space, 4 - PCHP.ToString().Length)}{PCHP}" +
               $"|{new string(space, 4 - PCOP.ToString().Length)}{PCOP}" +
               $"|{new string(space, 4 - ПCHP.ToString().Length)}{ПCHP}" +
               $"|{new string(space, 4 - ПCOP.ToString().Length)}{ПCOP}" +
               $"|{new string(space, 6 - P.ToString().Length)}{P}|";
    }
}