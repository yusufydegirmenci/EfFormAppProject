using EfFormAppProject.Data;
using EfFormAppProject.Models;

namespace EfFormAppProject
{
    public partial class Form1 : Form
    {
        private Student currentStudent;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (sender is not null && e is not null)
            {
                LoadComboBoxFromDatabase();
            }
        }

        private void LoadComboBoxFromDatabase()
        {
            using (var context = new ObsDbContext())
            {
                var siniflar = context.Classrooms.ToList();
                if (siniflar is not null)
                {
                    cbSinifSecimi.DataSource = siniflar;
                    cbSinifSecimi.DisplayMember = "ClassName";
                    cbSinifSecimi.ValueMember = "ClassId";

                    cbSinifSecimi.SelectedIndex = -1;
                }
            }
        }

        private void LoadStudentInfo(Student student)
        {
            if (student != null)
            {
                txtAd.Text = student.StudentName;
                txtSoyad.Text = student.StudentSurname;
                txtNumara.Text = student.StudentNumber;

                using (var context = new ObsDbContext())
                {
                    var siniflar = context.Classrooms.ToList();
                    cbSinifSecimi.DataSource = siniflar;
                    cbSinifSecimi.DisplayMember = "ClassName";
                    cbSinifSecimi.ValueMember = "ClassId";

                    var selectedClass = siniflar.FirstOrDefault(c => c.ClassId == student.ClassId);
                    if (selectedClass != null)
                    {
                        cbSinifSecimi.SelectedItem = selectedClass;
                    }
                    else
                    {
                        cbSinifSecimi.SelectedIndex = -1;
                    }
                }
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            string numara = txtNumara.Text.Trim();
            var selectedClass = cbSinifSecimi.SelectedItem as Classroom;

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(numara) || selectedClass == null)
            {
                MessageBox.Show("Lütfen boþ alan býrakmayýn");
                return;
            }

            using (var context = new ObsDbContext())
            {
                var classToUpdate = context.Classrooms.Find(selectedClass.ClassId);
                if (classToUpdate != null)
                {
                    var studentCountInClass = context.Students.Count(s => s.ClassId == selectedClass.ClassId);

                    if (studentCountInClass >= classToUpdate.Quota)
                    {
                        MessageBox.Show("Bu sýnýfýn kotasý dolmuþtur. Lütfen baþka bir sýnýf seçiniz.");
                        return;
                    }

                    var addStudent = new Student
                    {
                        StudentName = ad,
                        StudentSurname = soyad,
                        StudentNumber = numara,
                        ClassId = selectedClass.ClassId
                    };

                    context.Students.Add(addStudent);


                    context.SaveChanges();

                    MessageBox.Show("Baþarýyla kaydedildi!");
                    txtAd.Clear();
                    txtSoyad.Clear();
                    txtNumara.Clear();
                    cbSinifSecimi.SelectedIndex = -1;
                }
            }
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            string numara = txtNumara.Text.Trim();
            if (string.IsNullOrEmpty(numara))
            {
                MessageBox.Show("Lütfen numara giriniz");
                return;
            }

            using (var context = new ObsDbContext())
            {
                if (currentStudent != null && currentStudent.StudentNumber == numara)
                {
                    LoadStudentInfo(currentStudent);
                    return;
                }

                var find = context.Students.FirstOrDefault(s => s.StudentNumber == numara);

                if (find == null)
                {
                    MessageBox.Show("Bu numarada bir öðrenci bulunamadý.");
                    return;
                }

                currentStudent = find;

                LoadStudentInfo(find);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            string numara = txtNumara.Text.Trim();
            var selectedClass = cbSinifSecimi.SelectedItem as Classroom;

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(numara) || selectedClass == null)
            {
                MessageBox.Show("Lütfen boþ alan býrakmayýn");
                return;
            }

            using (var context = new ObsDbContext())
            {
                var studentToUpdate = context.Students.FirstOrDefault(s => s.StudentNumber == numara);
                if (studentToUpdate == null)
                {
                    MessageBox.Show("Bu numaraya ait bir öðrenci bulunamadý.");
                    return;
                }

                studentToUpdate.StudentName = ad;
                studentToUpdate.StudentSurname = soyad;
                studentToUpdate.ClassId = selectedClass.ClassId;

                context.SaveChanges();

                currentStudent = studentToUpdate;

                MessageBox.Show("Öðrenci baþarýyla güncellendi!");
                txtAd.Clear();
                txtSoyad.Clear();
                cbSinifSecimi.SelectedIndex = -1;
            }
        }
    }
}