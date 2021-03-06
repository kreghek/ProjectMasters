﻿namespace Assets.BL
{
    public class Skill
    {
        public bool IsLearnt { get; set; }
        public Job[] Jobs { get; set; }
        public SkillScheme Scheme { get; set; }

        public float GetPercentage()
        {
            if (Jobs is null)
            {
                return 0;
            }

            return (float)Jobs[0].CompleteSubTasksAmount / Scheme.RequiredJobs[0].CompleteSubTasksAmount;
        }
    }
}