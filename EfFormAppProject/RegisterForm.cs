using EfFormAppProject.Data;
using EfFormAppProject.Models;

namespace EfFormAppProject
{
    public partial class RegisterForm : Form
    {
        private Student currentStudent;
        Student std;
        public RegisterForm()
        {
            InitializeComponent();
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
                MessageBox.Show("Lütfen bo? alan b?rakmay?n");
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
                        MessageBox.Show("Bu s?n?f?n kotas? dolmu?tur. Lütfen ba?ka bir s?n?f seçiniz.");
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

                    MessageBox.Show("Ba?ar?yla kaydedildi!");
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
                    MessageBox.Show("Bu numarada bir ö?renci bulunamad?.");
                    return;
                }

                currentStudent = find;
                LoadStudentInfo(find);
                std = find;
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
                MessageBox.Show("Lütfen bo? alan b?rakmay?n");
                return;
            }

            using (var context = new ObsDbContext())
            {
                var studentToUpdate = context.Students.FirstOrDefault(s => s.StudentNumber == numara);
                if (studentToUpdate == null)
                {
                    MessageBox.Show("Bu numaraya ait bir öðrenci bulunamad?.");
                    return;
                }

                studentToUpdate.StudentName = ad;
                studentToUpdate.StudentSurname = soyad;
                studentToUpdate.ClassId = selectedClass.ClassId;

                context.SaveChanges();

                currentStudent = studentToUpdate;

                MessageBox.Show("Ö?renci ba?ar?yla güncellendi!");
                txtAd.Clear();
                txtSoyad.Clear();
                cbSinifSecimi.SelectedIndex = -1;
            }
        }
        private void btnDersSecimi_Click(object sender, EventArgs e)
        {
            if (std != null)
            {
                DersSecimi dersSecimi = new DersSecimi(std);
                dersSecimi.Show();
            }
            else
            {
                MessageBox.Show("Önce Ö?renci seçiniz");
            }
        }

    }
}