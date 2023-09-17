namespace lab1;

public class WorkTime
{
    public int StartPoint { get; }
    public int EndPoint { get; }

    public int Time { get; }

    public WorkTime(int startPoint, int endPoint, int time)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        Time = time;
    }
}