using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
    class Instruction
    {
        private TMN.Instruction i;
        public Instruction(TMN.Instruction instruction)
        {
            i = instruction;
        }

        public string AssignmentDate
        {
            get
            {
                return i.AssignmentDate == null ? null : i.AssignmentDate.Value.ToPersianDate().ToString("yyyy/MM/dd");
            }
        }

        public string ExcecutionDate
        {
            get
            {
                if (!i.ExecutionDate.HasValue)
                    return null;
                return i.ExecutionDate.Value.ToPersianDate().ToString("yyyy/MM/dd");
            }
        }

        public string TrafficType
        {
            get
            {
                return ((CenterTypes)Center.FromID(i.Destination.Value).CenterType).ToString();
            }
        }

        public string Type
        {
            get
            {
                return ((InstructionTypes)i.InstructionType).ToString();
            }
        }

        public string Executer
        {
            get
            {
                return i.User.FullName;
            }
        }

        public string Destination
        {
            get
            {
                return Center.FromID(i.Destination.Value).Name;
            }
        }

        public string Issuer
        {
            get
            {
                return i.Issuer;
            }
        }

        public string IssueDate
        {
            get
            {
                if (i.IssueDate == null)
                    return null;
                return i.IssueDate.Value.ToPersianDate().ToString("yyyy/MM/dd");
            }
        }

        public string InEffect
        {
            get
            {
                return i.Ineffect;
            }
        }

        public string Number
        {
            get
            {
                return i.Number;
            }
        }

        public string IsDone
        {
            get
            {
                return i.IsDone == true ? "*" : "";
            }
        }
    }
}
