using System.Collections.Generic;

namespace ProjectMasters.Models
{
    public class Project
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<FeatureTask> Tasks { get; set; }
    }

    public class FeatureTask
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<SkillScheme> RequiredSkills { get; set; }

        /// <summary>
        /// Задачи, которые нужно сделать перед началом/продолжением этой задачи.
        /// Сюда будут генерироваться подзадачи, возникающие по мере выполнения текущей.
        /// </summary>
        public List<FeatureTask> Blockers { get; set; }

        /// <summary>
        /// Прогресс в условных единицах.
        /// Прогресс увеличивается на 1, если все требуемые навыки равны 1.
        /// </summary>
        public float Progress { get; set; }

        /// <summary>
        /// Текущая емкость задачи в условных единицах. 1 == примерно 8ч.
        /// </summary>
        public float RealValue { get; set; }

        /// <summary>
        /// Оценка по задаче. В часах.
        /// </summary>
        public float Estimate { get; set; }

        /// <summary>
        /// Текущие ТРЗ.
        /// </summary>
        public float Logged { get; set; }

        /// <summary>
        /// Исполнители, назначенные на задачу.
        /// </summary>
        public List<Employee> Assignees { get; set; }
    }

    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class Skill
    {
        public long Id { get; set; }
        public SkillScheme Scheme { get; set; }
        public float Value { get; set; }
    }

    public class SkillScheme
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class Player
    {
        public long Id { get; set; }
        public int Energy { get; set; }
        public Project[] Projects { get; set; }
    }
}
