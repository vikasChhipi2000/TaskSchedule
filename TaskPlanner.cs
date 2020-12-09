using System.Collections.Generic;
using System.Linq;

namespace manpower
{
    class TaskPlanner : ITaskPlanner
    {
        private IRepository repository;
        public TaskPlanner(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Assignment> Execute()
        {
            //data of people,tasks and SKills
            var workers = repository.People.ToList();
            var skills = repository.Skills.ToList();
            var tasks = repository.Tasks.ToList();

            // sorted task and skill
            var sortedTask = SortTask(tasks);
            var skillMap = MakeMap(workers, skills);

            // checkTask and checkWorker initialization 
            Dictionary<int, bool> checkTask = new Dictionary<int, bool>();
            foreach(Task t in sortedTask)
            {
                checkTask[t.Id] = false;
            }
            Dictionary<int, bool> checkWorker = new Dictionary<int, bool>();
            foreach(Person p in workers)
            {
                checkWorker[p.Id] = false;
            }

            int day = 1;
            List<Assignment> assignments = new List<Assignment>(sortedTask.Count);
            while(!AllTaskComplete(checkTask))
            {
                ResetCheckWorker(checkWorker);
                int i = 0;
                AssignTask(sortedTask, skillMap, checkTask, checkWorker, i, day, assignments);
                day++;
            }
            return assignments;
        }

        //assign task to worker
        private void AssignTask(List<Task> sortedTask, Dictionary<int, List<Person>> skillMap, Dictionary<int, bool> checkTask, Dictionary<int, bool> checkWorker, int i, int day, List<Assignment> assignments)
        {
            while (!AllWorkerFull(checkWorker) && i < sortedTask.Count)
            {
                if (!checkTask[sortedTask[i].Id])
                {
                    int skillNeededId = sortedTask[i].SkillRequired.Id;

                    foreach (var person in skillMap[skillNeededId])
                    {
                        if (!checkWorker[person.Id])
                        {
                            checkTask[sortedTask[i].Id] = true;
                            checkWorker[person.Id] = true;

                            Assignment assignment = new Assignment
                            {
                                Task = sortedTask[i],
                                Person = person,
                                Day = day
                            };

                            assignments.Add(assignment);
                            break;
                        }
                    }
                }
                i++;
            }
            return;
        }

        // check that all worker got tasks for a day 
        private bool AllWorkerFull(Dictionary<int, bool> checkWorker)
        {
            foreach (var key in checkWorker.Keys.ToList())
            {
                if (!checkWorker[key])
                {
                    return false;
                }
            }
            return true;
        }

        // check all task are assign
        private bool AllTaskComplete(Dictionary<int, bool> checkTask)
        {
            foreach(var key in checkTask.Keys.ToList())
            {
                if (!checkTask[key])
                {
                    return false;
                }
            }
            return true;
        }

        // reset checkWorker for next day
        private void ResetCheckWorker(Dictionary<int ,bool> checkWorker)
        {
            foreach (var key in checkWorker.Keys.ToList())
            {
                checkWorker[key] = false;
            }

        }


        // sort task on the base of priority
        private List<Task> SortTask(IEnumerable<Task> tasks)
        {
            List<Task> sortedTask = new List<Task>();
            TaskSort taskSort = new TaskSort();
            int index = 0;
            foreach(Task t in tasks)
            {
                if (t.IsPriority)
                {
                    sortedTask.Add(t);
                    index++;
                }
            }
            sortedTask.Sort(0, index, taskSort);
            foreach(Task t in tasks)
            {
                if (!t.IsPriority)
                {
                    sortedTask.Add(t);
                }
            }

            // sorting task on the base of skillpoint (rare task come on top)
            sortedTask.Sort(index, tasks.Count()-index, taskSort);
            return sortedTask;
        }

        // making dictionary of skill and list of person who has that skill
        private Dictionary<int, List<Person>> MakeMap(List<Person> workers, IEnumerable<Skill> skills)
        {
            Dictionary<int ,List<Person>> skillMap = new Dictionary<int, List<Person>>();
            foreach(Skill s in skills)
            {
                skillMap[s.Id] = new List<Person>();
            }

            foreach(Person p in workers)
            {
                HashSet<Skill> personSkills = p.Skills;
                foreach(Skill skill in personSkills)
                {
                    skillMap[skill.Id].Add(p);
                }
            }

            // sorting the list person on the based of sum of all skillpoints
            foreach(var key in skillMap.Keys.ToList())
            {
               skillMap[key].Sort((x, y) => x.sumOfAllSkill().CompareTo(y.sumOfAllSkill()));
            }
            return skillMap;
        }
    }

    // comparer class for task sort
    class TaskSort : IComparer<Task>
    {
        public int Compare(Task x, Task y)
        {
            if(x==null || y == null)
            {
                return 0;
            }
            if(x.SkillRequired.SkillLevel == y.SkillRequired.SkillLevel)
            {
                return -1*(x.Id - y.Id);
            }
            return (y.SkillRequired.SkillLevel - x.SkillRequired.SkillLevel);
        }
    }
}
