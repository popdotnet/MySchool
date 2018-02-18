using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>

public class GetDataByDelegate<T>
{
    internal delegate T GetDataDelegate<I>(I i);
    internal delegate T GetDataFromListDelegate<I>(I i, List<T> Tlist);


    internal delegate T GetDataDelegate<Year, Serial>(Year i, Serial s)
        where Year : struct
        where Serial : struct;

}

