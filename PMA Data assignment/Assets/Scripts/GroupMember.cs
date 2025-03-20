using UnityEngine;
using System;
using System.Collections.Generic;


// for .json serialization
[Serializable]
    public struct Member
    {
        public string name;
        public string dateOfBirth;
        public string favColor;

        public Member(string name, string dateOfBirth, string favColor)
        {
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.favColor = favColor;
        }
    }

// for .json serialization
[Serializable]
public class GroupMembers
{
    public List<Member> groupMembers;
}
