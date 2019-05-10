using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{



    public enum Enquiry
    {
        [Description("NONE")]
        NONE = 0,

        [Description("Read/Unread")]
        Yes = 1,

        [Description("Read/Unread")]
        No = 2

       
    }

    public enum Sex
    {
        [Description("NONE")]
        NONE = 0,

        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2,

        [Description("Other")]
        Other = 3

    }

    public enum ReadStatus
    {
        [Description("None")]
        None = 0,

        [Description("Read")]
        Read = 1,

        [Description("Un-Read")]
        UnRead = 2
    }

    public enum MessageStatus
    {
        [Description("None")]
        None = 0,

        [Description("Inbox")]
        Inbox = 1,

        [Description("Sent")]
        Sent = 2,

        [Description("Draft")]
        Draft = 3,

        [Description("Trash")]
        Trash = 4

       
    }


    public enum ReportType
    {
        [Description("None")]
        None = 0,

        [Description("Birth Report")]
        Birth = 1,

        [Description("Death Report")]
        Death = 2,

        [Description("Operation Report")]
        Operation = 3,

        [Description("Investigation Report")]
        Investigation = 4
    }

    public enum BedStatus
    {
        [Description("None")]
        None = 0,

        [Description("In")]
        In = 1,

        [Description("Out")]
        Out = 2
    }

    public enum EntityStatus
    {
        [Description("None")]
        None = 0,

        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        Inactive = 2
    }

    public enum DeptStatus
    {
        [Description("None")]
        None = 0,

        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        Inactive = 2
    }


    public enum AccountType
    {
        [Description("None")]
        None = 0,

        [Description("Debit")]
        Debit = 1,

        [Description("Credit")]
        Credit = 2,

      
    }


    public enum AppointmentType
    {
        [Description("None")]
        None = 0,

        [Description("Assign By All")]
        AssignByAll = 1,

        [Description("Assign By Doctor")]
        AssignByDoctor = 2,

        [Description("Assign By Receptionist")]
        AssignByReceptionist = 3
    }

    public enum Days
    {
        [Description("None")]
        None = 0,

        [Description("Sunday")]
        Sunday = 1,

        [Description("Monday")]
        Monday = 2,

        [Description("Tuesday")]
        Tuesday = 3,

        [Description("Wednesday")]
        Wednesday = 4,

        [Description("Thursday")]
        Thursday = 5,

        [Description("Friday")]
        Friday = 6,

        [Description("Saturday")]
        Saturday = 7
    }



}
