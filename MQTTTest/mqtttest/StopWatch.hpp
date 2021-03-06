//----------------------------------------------------------------------------
//
//  $Workfile: StopWatch.hpp$
//
//  $Revision: X$
//
//  Project:    multiple
//
//                            Copyright (c) 2017
//                                Jim Wright
//                            All Rights Reserved
//
//  Modification History:
//  $Log:
//  $
//
//----------------------------------------------------------------------------
#pragma once

//----------------------------------------------------------------------------
//  Class Declarations
//----------------------------------------------------------------------------
//
// Class Name: StopWatch
// 
// Purpose:
//      Count time and tell client if the stop watch has expired
//
//----------------------------------------------------------------------------
class StopWatch
{
    private:
        long mLastTime; //The last time the watch was reset
        long mWaitTime; //The time in millis to wait

        //----------------------------------------------------------------------------
        //  Purpose:
        //      Return the current time in millis
        //
        //  Notes:
        //      None
        //
        //----------------------------------------------------------------------------
        long Now(void);

    public:
        //----------------------------------------------------------------------------
        //  Purpose:
        //      Constructor for waiting a second
        //
        //  Notes:
        //      None
        //
        //----------------------------------------------------------------------------
        StopWatch();

        //----------------------------------------------------------------------------
        //  Purpose:
        //      Constructor for waiting a set time
        //
        //  Notes:
        //      None
        //
        //----------------------------------------------------------------------------
        StopWatch(int waitTime);

        //----------------------------------------------------------------------------
        //  Purpose:
        //      Change the time to wait
        //
        //  Notes:
        //      None
        //
        //----------------------------------------------------------------------------
        void SetTime(int waitTime);

        //----------------------------------------------------------------------------
        //  Purpose:
        //      Has the stop watch expired
        //
        //  Notes:
        //      None
        //
        //----------------------------------------------------------------------------
        bool IsExpired(void);

        //----------------------------------------------------------------------------
        //  Purpose:
        //      Reset the stop watch
        //
        //  Notes:
        //      None
        //
        //----------------------------------------------------------------------------
        void Reset(void);

        //----------------------------------------------------------------------------
        //  Purpose:
        //      Return the milliseconds left in the stop watch
        //
        //  Notes:
        //      None
        //
        //----------------------------------------------------------------------------
        long GetTimeLeft(void);
};

