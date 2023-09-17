namespace lab1;

public class EventTableLine
{
    public int Event { get; set; }
    public int To { get; set; }
    public int EarlyDeadline { get; set; }
    public int LateDeadline { get; set; }
    public int Reserve { get; set; }
    
    public override string ToString()
    {
        var space = ' ';
        return $"|{new string(space, 7 - Event.ToString().Length)}{Event}" +
               $"|{new string(space, 11 - EarlyDeadline.ToString().Length)}{EarlyDeadline}" +
               $"|{new string(space, 12 - LateDeadline.ToString().Length)}{LateDeadline}" +
               $"|{new string(space, 6 - Reserve.ToString().Length)}{Reserve}";
    }
}