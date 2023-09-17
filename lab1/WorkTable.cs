namespace lab1;

public class WorkTable
{
    public List<WorkTableLine> Lines { get; set; }

    public WorkTable(List<int> inElements, List<int> outElements, List<int> to, List<WorkTime> workTimes)
    {
        var orderedWorkTimes = OrderWorkTimes(workTimes, to);
        var list = new List<WorkTableLine>();
        foreach (var workTime in orderedWorkTimes)
        {
            var workTableLine = new WorkTableLine();
            workTableLine.Time = workTime.Time;
            workTableLine.Work = $"{workTime.StartPoint}_{workTime.EndPoint}";
            workTableLine.WorkTime = workTime;
            if (inElements.Contains(workTime.StartPoint))
            {
                workTableLine.PCHP = 0;
                workTableLine.PCOP = workTime.Time;
            }
            else
            {
                workTableLine.PCHP = list.Where(element => element.WorkTime.EndPoint == workTime.StartPoint).Select(_ => _.PCOP).Max();
                workTableLine.PCOP = workTableLine.PCHP + workTableLine.Time;
            }
            list.Add(workTableLine);
        }

        var criticalWayLength = list.MaxBy(line => line.PCOP)!.PCOP;

        foreach (var line in list)
        {
            if (outElements.Contains(line.WorkTime.EndPoint))
            {
                line.ПCOP = criticalWayLength;
                line.ПCHP = criticalWayLength - line.Time;
                line.P = line.ПCOP - line.PCOP;
            }
        }

        var checkedEndPoint = new List<int>();
        while (list.Any(line => line.ПCOP == 0))
        {
            var currentMin = list.Where(line => line.ПCOP != 0 && checkedEndPoint.Contains(line.WorkTime.StartPoint) is false).MinBy(_ => _.ПCHP)!.ПCHP;
            var currentEndPoint = list.Where(line => line.ПCOP != 0 && checkedEndPoint.Contains(line.WorkTime.StartPoint) is false).MinBy(_ => _.ПCHP)!.WorkTime.StartPoint;

            foreach (var line in list.Where(_ => _.WorkTime.EndPoint == currentEndPoint))
            {
                if (line.ПCOP == 0)
                {
                    line.ПCOP = currentMin;
                    line.ПCHP = currentMin - line.Time;
                    line.P = line.ПCOP - line.PCOP;
                }
            }
            
            checkedEndPoint.Add(currentEndPoint);
        }

        Lines = list;
    }

    private List<WorkTime> OrderWorkTimes(List<WorkTime> workTimes, List<int> to)
    {
        var orderedWorkTimes = new List<WorkTime>();
        var counter = 0;

        while (orderedWorkTimes.Count != workTimes.Count)
        {
            foreach (var workTime in workTimes.ToList().OrderBy(_ => _.StartPoint).ThenBy(_ => _.EndPoint))
            {
                for (var i = 0; i < to.Count; i++)
                {
                    if (counter == to[i])
                    {
                        if (workTime.StartPoint == i + 1)
                        {
                            orderedWorkTimes.Add(workTime);
                        }
                    }
                }
            }

            counter++;
        }

        return orderedWorkTimes;
    }
}