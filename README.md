# SNic Assignemnt by: Vikas Chhipi 


## Assignment : 
Developing a manpower planning class that assigns work to employees based on their skills and the priority of the tasks. Given the conditions that:
* Each task takes a full day to perform.
* Each task requires a single person to perform it.

## Algorithmic Approach :
The approach taken is to assign the tasks by sorting them according to the rarity of the skills required and the priority :
1. Create a dictionary of skill id and list of person who have that skill.
2. Sort list on basis of sum of the skill points of the skills that person has so that the person with 
less rare skills comes first in list then the person who has more and rare skills comes in the list.
3. Assign the tasks with the priority ones being assigned first

## Pseudocode:

To assign tasks to people we create a dictionary of skill ids and list of persons who have that skill.
Then we sort the list on basis of sum of the skill points of the skill that person has so the person with less rare skills come first in list then the person who has more and rare skills comes later.

Task is then sort on priority: high priority tasks come first and non priority tasks come later. We also internally sort priority and non priority tasks on basis of skill point of skill required so we assign the rare skill tasks first then normal skill tasks.

In execute method we run the loop on tasks until all task are not assigned. For all unassigned tasks we find the skill required and find the list of people that have that skill (using dictionary) and then find a person that is not assigned to any task on that day in the list.

Then we assign the task to that person and move to the next task and so on until all people are assigned on that day or task list ends.
If all tasks are not assigned then we repeat the above for the next day.

## Verification Of Results :
The points to consider while assigning are:
1. The number of days it takes to complete all the high priority jobs
2. The number of days it takes to complete the whole list of jobs
3. How effectively each person’s skill set it being utilized
4. How long it takes to generate the plan

### 1. Small Task:
By analysing the CSV file output, we can verify and find the answers to the above questions.
1. By finding the maximum value of the day column, we can see that it takes **3** days with the high priority tasks being done in **1** day.
2. As their are 6 people and 14 tasks, the minimum no of days required is (14//6 =2) more than 2 days and as the work is done in the minimum time, we can say that each person's skill set is being used effectively.
3. It takes appox **18ms** to generate the plan for this task assignment.

### 2. Large Task:
By analysing the CSV file output, we can verify and find the answers to the above questions.
1. By finding the maximum value of the day column, we can see that it takes **50** days with the high priority tasks being done in **6** days.
2. As their are 6 people and 300 tasks, the minimum no of days required is (300//6 =50) 50 days and as the work is done in the minimum time, we can say that each person's skill set is being used effectively.
3. It takes appox **22ms** to generate the plan for this task assignment.

**Note:- add a out folder in bin\Debug\netcoreapp2.2\data before running the program.**






