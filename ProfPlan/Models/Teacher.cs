﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfPlan.Models
{
    public class Teacher : INotifyPropertyChanged
    {
        private string _institute;
        public string Institute
        {
            get { return _institute; }
            set
            {
                if (_institute != value)
                {
                    _institute = value;
                    OnPropertyChanged(nameof(Institute));
                }
            }
        }
        private string _department;
        public string Department
        {
            get { return _department; }
            set
            {
                if (_department != value)
                {
                    _department = value;
                    OnPropertyChanged(nameof(Department));
                }
            }
        }
        private string _lastname;
        public string LastName
        {
            get { return _lastname; }
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        private string _middleName;
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged(nameof(MiddleName));
                }
            }
        }
        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                }
            }
        }
        private string _academicDegree;
        public string AcademicDegree
        {
            get { return _academicDegree; }
            set
            {
                if (_academicDegree != value)
                {
                    _academicDegree = value;
                    OnPropertyChanged(nameof(AcademicDegree));
                }
            }
        }
        private string _workload;
        public string Workload
        {
            get { return _workload; }
            set
            {
                if (_workload != value)
                {
                    _workload = value;
                    OnPropertyChanged(nameof(Workload));
                }
            }
        }
        
        public Teacher() { }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
