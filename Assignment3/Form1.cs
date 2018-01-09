using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3
{
    

    public partial class Form1 : Form
    {   
        /// <summary>
        /// represent the number of students
        /// </summary>
        int row;
        /// <summary>
        /// represents the number of assignments
        /// </summary>
        int columns;
        /// <summary>
        /// identifies the assignment
        /// </summary>
        int assignmentNum;
        /// <summary>
        /// score for the particular assignment
        /// </summary>
        int score;
        /// <summary>
        /// used to check if number of students entered in textbox is parsable
        /// </summary>
        bool checkNumStudents;
        /// <summary>
        /// used to check if number of assignments entered in textbox is parsable
        /// </summary>
        bool checkNumAssignments;
        /// <summary>
        /// used to check if assignment number entered in textbox is parsable
        /// </summary>
        bool checkValidAssignment;
        /// <summary>
        /// used to check if score entered in textbox is parsable
        /// </summary>
        bool checkValidScore;
        /// <summary>
        /// array to hold student names
        /// </summary>
        string[] studentNames;
        /// <summary>
        /// array created to hold average grade
        /// </summary>
        float[] prcntGrd;
        /// <summary>
        /// array used to hold grades
        /// </summary>
        int[,] grades;
        /// <summary>
        /// used as a marker to identify current element
        /// </summary>
        int currentIdx;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Method checks for all valid input and enables the groupboxes and initializes arrays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subCountsBut_Click(object sender, EventArgs e)
        {
            //Checking if number of studends and number of assignments textboxes can be parsed to integers
            checkNumStudents = Int32.TryParse(NumStudents.Text, out row); 
            checkNumAssignments = Int32.TryParse(NumAssignments.Text, out columns);     
  
            //If user enters the number of student but more then the acceptable limit give them a warning
            if (checkNumStudents)
            {
                if (row > 0 && row < 11)
                {
                    sWrnLbl.Visible = false;
                    sWrnLbl.Refresh();
                }
                else
                {
                    sWrnLbl.Text = "Enter Integer 1-10";
                    sWrnLbl.Visible = true;
                    sWrnLbl.Refresh();
                }
            }
            //If user enters the number of assignment but more then then the acceptable limit give them a warning
            if (checkNumAssignments)
            {
                if (columns > 0 && columns < 100)
                {
                    asWrnLbl.Visible = false;
                    asWrnLbl.Refresh();
                }
                else
                {
                    asWrnLbl.Text = "Enter Integer 1-99";
                    asWrnLbl.Visible = true;
                    asWrnLbl.Refresh();
                }
            }
           //If the user leaves the number of students textbox empty or doesn't enter an integer give them a warning
            if (string.IsNullOrWhiteSpace(NumStudents.Text) || (!checkNumStudents)) 
            {
                sWrnLbl.Text = "Please Enter an Integer";
                sWrnLbl.Visible = true;
                sWrnLbl.Refresh();
            }
            //If the user leaves the number of assignments textbox empty or doesn't enter an integer give them a warning
            if (string.IsNullOrWhiteSpace(NumAssignments.Text) || (!checkNumAssignments))
            {
                asWrnLbl.Text = "Please Enter an Integer";
                asWrnLbl.Visible = true;
                asWrnLbl.Refresh();
            }
            //If both the number of student and number of assignments passes then enable the group boxes and create arrays
            if (checkNumAssignments && checkNumStudents && columns > 0 && columns < 100 && row > 0 && row < 11)
            {
                startFillArrays();
                //Enable the groupBoxes and buttons
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                scoreBtn.Enabled = true;
                richTextBox1.Refresh();
                groupBox1.Enabled = false;
            }
      

         }
        /// <summary>
        /// Dissables all groupboxes except the first groupbox where the numbers of student is entered and asignment size
        /// </summary>
        private void resetAll()
        {
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            scoreBtn.Enabled = false;
            groupBox1.Enabled = true;
        }
        /// <summary>
        /// This will update the student name label with the name stored in studentArrays at element stored in currentIdx  
        /// </summary>
        private void updateStudentLabel()
        {
            label3.Text = studentNames[currentIdx];
            label3.Refresh();
            label6.Visible = false;
            label6.Refresh();
        }
        /// <summary>
        /// Creates one dimensonal and two dimensional array that will also fill array with generic student #'s and fill assignments with 0's
        /// </summary>
        private void startFillArrays()
        {
            currentIdx = 1;
            label4.Text = "Enter Assignment Number 1-" + columns.ToString(); 
            //initializing arrays
            studentNames = new string[row + 1]; //added an extra element so I could skip element zero
            grades = new int[row + 1, columns + 1]; //added an extra element so I could skip element zero

            //filling student names with default names
            for (int i = 1; i <= row; i++)
            {
                studentNames[i] = "Student #" + i.ToString();
            }
            //filling assignments with default 0
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= columns; j++)
                {
                    grades[i, j] = 0;
                }
            }
        }
        /// <summary>
        /// Enters a grade and returns a letter grade
        /// </summary>
        /// <param name="g"></param>
        /// <returns>string letter grade</returns>
        private string letterGrade(float g)
        {
            if (g > 92.9)
                return "A";
            else if (g > 89.9)
                return "A-";
            else if (g > 86.9)
                return "B+";
            else if (g > 82.9)
                return "B";
            else if (g > 79.9)
                return "B-";
            else if (g > 76.9)
                return "C+";
            else if (g > 72.9)
                return "C";
            else if (g > 69.9)
                return "C-";
            else if (g > 66.9)
                return "D+";
            else if (g > 62.9)
                return "D";
            else if (g > 60)
                return "D-";
            else
                return "E";               
        }

        
        /// <summary>
        /// Jumps to the first student stored in the studentNames array and updates the student name label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            currentIdx = 1; //set index to first element which is really the second element since I skipped over element 0
            updateStudentLabel();
        }

        /// <summary>
        /// Jumps to the previous student stored in the studentNames array and updates the student name label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (currentIdx == 1) //If at the first student do go to the previous
            {
                currentIdx = 1;
                updateStudentLabel();
            }
            else if (currentIdx > 1)//If not at the first student go to previous
            {
                --currentIdx;       //decrement currentidx before updating label
                updateStudentLabel();
            }
        }

        /// <summary>
        /// Jumps to the previous student stored in the studentNames array and updates thte student name label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (currentIdx == row)  //If at the last student don't go to next student
            {
                currentIdx = row;   //increment current index before updating label
                updateStudentLabel();
            }
            else if (currentIdx < row)//If  not at the last student go to the next student
            {
                ++currentIdx;       //Increment current index before update label
                updateStudentLabel();
            }
        }

        /// <summary>
        /// Jumps to the last student stored in the studentNames array and upadte the student name label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            currentIdx = row;  //set index to last element
            updateStudentLabel();
        }
        /// <summary>
        /// Validates proper input of student names and gives warning if input is invalid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StudentName.Text))
            {
                label6.Visible = false;
                studentNames[currentIdx] = StudentName.Text;  //take string from textbox and insert into studentname array
                StudentName.Text = String.Empty; //clear box after taking string
                updateStudentLabel();
            }
            else
                label6.Visible = true; //if input is not valid display invalid message

        }
        /// <summary>
        /// Will check validate input in the assignment number and score textbox.  Will alert user of invalid input.  If input is valid will
        /// place values in the grades array
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            checkValidAssignment = Int32.TryParse(textBox4.Text, out assignmentNum); //parsing assignment number and checking validity
            checkValidScore = Int32.TryParse(textBox5.Text, out score); //parsing score input and checking validity

            if (checkValidAssignment)
            {
                //checking assignment number is between 1 and the number of assignments the user entered 
                if (assignmentNum > 0 && assignmentNum <= columns)
                {
                    label7.Visible = false;     //turn off alert if valid input
                    label7.Refresh();
                }
                //if invalid altert user of invalid input with a label
                else
                {
                    label7.Text = "Enter Assignment # within range";
                    label7.Visible = true;
                    label7.Refresh();
                }
            }

            if (checkValidScore)
            {   //checking score is between 0 and 100
                if (score >= 0 && score <= 100)
                {
                    label8.Visible = false; //remove alet label
                    label8.Refresh();
                }
                else
                {   //if invalid alter us of invalid with a label
                    label8.Text = "Enter score 0-100";
                    label8.Visible = true;
                    label8.Refresh();
                }
            }
            //checking for empty space prompt user to enter integer
            if (string.IsNullOrWhiteSpace(textBox4.Text) || (!checkValidAssignment))
            {
                label7.Text = "Please Enter an Integer";
                label7.Visible = true;
                label7.Refresh();
            }
            //checking for empty space prompt user to enter integer
            if (string.IsNullOrWhiteSpace(textBox5.Text) || (!checkValidScore))
            {
                label8.Text = "Please Enter an Integer";
                label8.Visible = true;
                label8.Refresh();
            }
            //if all condition are met then add the score to the grades array
            if (checkValidAssignment && checkValidScore && score >= 0 && score <= 100 && assignmentNum > 0 && assignmentNum <= columns)
            {
                grades[currentIdx, assignmentNum] = score;
                textBox4.Text = String.Empty;
                textBox5.Text = String.Empty;
            }
        }
        /// <summary>
        /// Will print the data in the studentnames and grades array
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scoreBtn_Click(object sender, EventArgs e)
        {
            //I'm clearing the 0 element in the array in case the user clicks the scoreBtn twice so it wont add the previous sum from the first click
            for(int i =1; i<=row; i++)
            {
                grades[i, 0] = 0;
            }
            //Adding all the scores in the array and placing it in element 0
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= columns; j++)
                {
                    grades[i, 0] += grades[i, j];
                }
            }
            //creating array to hold percentages
            prcntGrd = new float[row + 1];

            //calculating average of all grades and placing them in prcntGrd array
            for (int i = 1; i <= row; i++)
            {
                prcntGrd[i] = (float)grades[i, 0] / (float)columns;
            }

            //creating header for textbox
            richTextBox1.Text = "Student\t\t";
            for(int i = 1; i <= columns; i++)
            {
                richTextBox1.Text += "#" + i +"\t";
            }
            richTextBox1.Text += "AVG\tGRADE\n";

            //printing scores and average and letter grade for each student
            for (int i = 1; i<=row; i++)
            {
                richTextBox1.Text += studentNames[i] + "\t";
                for (int j = 1; j<=columns; j++)
                {
                    richTextBox1.Text += grades[i, j].ToString() + "\t";
                    if(j == columns)
                    {
                        richTextBox1.Text += prcntGrd[i].ToString() + "\t" + letterGrade(prcntGrd[i]) + "\n";
                    }
                } 
               

            }
        }
        /// <summary>
        /// Reset program back to initial state 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            resetAll();
        }
    }
}
