using System;
using System.Collections.Generic;
using Isu.Services;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class ExtraGroup : Group
    {
        public ExtraGroup(string groupName, List<Student> students)
            : base(groupName, students)
        {
        }

        public ExtraGroup(string groupName)
            : base(groupName)
        {
        }

        public override void CheckGroupName(string groupName)
        {
            if (groupName.Length == 5 && char.IsLetter(groupName[0]))
            {
                for (int i = 2; i < 5; i++)
                {
                    try
                    {
                        int int32 = Convert.ToInt32(groupName[i]);
                    }
                    catch (FormatException)
                    {
                        throw new InvalidGroupException();
                    }
                }
            }
            else
            {
                throw new InvalidGroupException();
            }
        }
    }
}