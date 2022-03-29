
using System;
using System.IO;
using System.Collections.Generic;
using static System.Console;

namespace Bme121
{
    // Class to represent a water-quality sample for a New York City beach.
    // Data comes from a CSV file. One sample line is shown below, to illustrate the format.
    //     NP2109080725-1.2,09/08/2021,SOUTH BEACH,Center,53
    // The fields are sample identifier, date in mm/dd/yyyy format, beach name
    // sample location, and enterococci level in MPN/100 mL.
    public enum Value 
    { 
        Left, 
        Center, 
        Right
    };
    
    class Sample : IComparable< Sample > 
    {
        
            public string Date          {get; private set; }
            public string BeachName     {get; private set; }
            public string Location      {get; private set; }
            public double Enterococci   {get; private set; }
            public int Year             {get {return int.Parse( Date.Split("/")[2]);}}
            public int Month            {get {return int.Parse( Date.Split("/")[0]);}}
            public int Day              {get {return int.Parse( Date.Split("/")[1]);}}
            
            public Sample( string str)
            {
                string[] stringArray = str.Split(',');
                Date = stringArray[1];
                BeachName = stringArray[2];
                Location = stringArray[3];
                Enterococci = double.Parse(stringArray[4]);
            }
            
            public int CompareTo( Sample other )
            {
                
                int thisLocation  = ( int ) Enum.Parse( typeof ( Value ), Location );
                int otherLocation = ( int ) Enum.Parse( typeof ( Value ), other.Location );
                
                int sortYear = Year.CompareTo ( other.Year );
                int sortMonth = Month.CompareTo ( other.Month );
                int sortDay = Day.CompareTo ( other.Day );
                int sortBeachName = BeachName.CompareTo ( other.BeachName );
                int sortLocation = thisLocation.CompareTo ( otherLocation );
                
                
                if( sortBeachName != 0 ) return sortBeachName;
                else if( sortLocation != 0 ) return sortLocation;
                else if( sortYear != 0 ) return sortYear;
                else if( sortMonth != 0 )return sortMonth;
                else return sortDay;
             }   
            
    }           
                
    // Program tpublic double enterococcio report New York City beach water samples where the
    // enterococci level is 5000 MPN/100 mL or more.
    
    static class Program
    {
        static void Main( )
        {
            WriteLine( );
            
            //read
            using StreamReader r = new StreamReader( "DOHMH_Beach_Water_Quality_Data.csv" );

            List< Sample > sampleList = new List< Sample >( );
            
            while( ! r.EndOfStream )
            {
                Sample s = new Sample( r.ReadLine( ) ! );
                
                sampleList.Add( s );
            }
            //Put list in array and sort auto beachName
            Sample[] listArray = sampleList.ToArray();
            Array.Sort(listArray);
            
            foreach(Sample s in listArray)
            {
                if( s.BeachName == "WOLFE'S POND PARK" && s.Year == 2021 && (s.Month == 8 || s.Month == 9)  )
                {
                    Write( $" {s.Year} {s.Month,2} {s.Day,2} {s.BeachName,-33} {s.Location,6}" );
                    WriteLine( $" {s.Enterococci,6:n0} MPN/100 mL" );
                }
            }
            WriteLine( );
        }
    }
}
