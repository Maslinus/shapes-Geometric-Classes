using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static geometric_shapes.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace geometric_shapes
{
    public partial class Form1 : Form
    {
        public static int count = 1;

        List<Figure> figures = new List<Figure>();

        double grid = 20;

        public Form1()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            Axis ax = chart1.ChartAreas[0].AxisX;
            ax.Minimum = -grid;
            ax.Maximum = grid;
            ax.Crossing = 0;


            ax.MajorGrid.Enabled = false;
            ax.MinorGrid.Enabled = false;

            ax = chart1.ChartAreas[0].AxisY;
            ax.Minimum = -grid;
            ax.Maximum = grid;
            ax.Crossing = 0;

            ax.MajorGrid.Enabled = false;
            ax.MinorGrid.Enabled = false;


            chart1.Series.Add(new Series());
            chart1.Series[0].Color = Color.Transparent;
            chart1.Series[0].Points.AddXY(0, 0);

            string[] itemsFigures = { "Отрезок", "Ломаная", "Окружность", "Многоугольник", "Треугольник", "Четырёхугольник", "Прямоугольник", "Трапеция" };
            comboBoxAdd.Items.AddRange(itemsFigures);

            string[] itemsMove = { "Сдвиг", "Поворот", "Симметрия" };
            comboBoxMovement.Items.AddRange(itemsMove);

            string[] itemsSymAxis = { "x", "y" };
            comboBoxSymAxis.Items.AddRange(itemsSymAxis);

        }

        public class Figure
        {
            public IShape shape;
            public int number;
            Chart chart;

            public Figure(IShape shape, Chart chart)
            {
                this.shape = shape;
                number = count;
                this.chart = chart;
            }

            public void Draw()
            {
                shape.Draw(chart, number);
                count++;
            }

            public void Clear()
            {
                chart.Series[number].Points.Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            comboBoxChoseMoveFigure.Items.Clear();
            comboBoxDeleteFigure.Items.Clear();
            comboBoxCrossFigure1.Items.Clear();
            comboBoxCrossFigure2.Items.Clear();

            count = 1;

            chart1.Series.Add(new Series());
            chart1.Series[0].Color = Color.Transparent;
            chart1.Series[0].Points.AddXY(0, 0);

            textBoxRes.Visible = false;
            labelRes.Visible = false;
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            if (!textBoxRes.Visible)
            {
                textBoxRes.Visible = true;
                labelRes.Visible = true;
            }

            double sum_square = 0;

            foreach (var v in figures)
            {
                sum_square += v.shape.Square();
            }

            textBoxRes.Clear();
            textBoxRes.Text += Math.Round(sum_square, 2);

            labelRes.Text = "Площадь:";
        }

        private void btnPerimeter_Click(object sender, EventArgs e)
        {
            if (!textBoxRes.Visible)
            {
                textBoxRes.Visible = true;
                labelRes.Visible = true;
            }
            double sum_length = 0;

            foreach (var v in figures)
            {
                sum_length += v.shape.Length();
            }

            textBoxRes.Clear();
            textBoxRes.Text += Math.Round(sum_length, 2);

            labelRes.Text = "Периметр:";
        }

        private void btnAddFigure_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxRes.Visible = false;
                labelRes.Visible = false;

                switch (comboBoxAdd.SelectedItem.ToString())
                {
                    case "Отрезок":
                        figures.Add(new Figure(new Segment(new Point2D(new double[] { double.Parse(textBox1X.Text), double.Parse(textBox1Y.Text) }), new Point2D(new double[] { double.Parse(textBox2X.Text), double.Parse(textBox2Y.Text) })), chart1));
                        figures.Last().Draw();
                        break;

                    case "Окружность":
                        figures.Add(new Figure(new Circle(new Point2D(new double[] { double.Parse(textBox1X.Text), double.Parse(textBox1Y.Text) }), double.Parse(textBoxRadius.Text)), chart1));
                        figures.Last().Draw();
                        break;

                    case "Ломаная":
                    case "Многоугольник":
                    case "Треугольник":
                    case "Четырёхугольник":
                    case "Прямоугольник":
                    case "Трапеция":
                        int points_count = (int)numericUpDownPointsCount.Value;
                        Point2D[] pf = new Point2D[points_count];

                        for (int i = 1; i <= points_count; i++)
                        {
                            string x_value = "textBox" + i + "X";
                            TextBox x = Controls.Find(x_value, true).FirstOrDefault() as TextBox;
                            string y_value = "textBox" + i + "Y";
                            TextBox y = Controls.Find(y_value, true).FirstOrDefault() as TextBox;

                            pf[i - 1] = new Point2D(new double[] { double.Parse(x.Text), double.Parse(y.Text) });
                        }

                        string type = comboBoxAdd.SelectedItem.ToString();

                        if (type == "Ломаная")
                            figures.Add(new Figure(new Polyline(pf), chart1));
                        if (type == "Многоугольник")
                            figures.Add(new Figure(new NGon(pf), chart1));
                        if (type == "Четырёхугольник")
                            figures.Add(new Figure(new QGon(pf), chart1));
                        if (type == "Треугольник")
                            figures.Add(new Figure(new TGon(pf), chart1));
                        if (type == "Трапеция")
                            figures.Add(new Figure(new Trapeze(pf), chart1));
                        if (type == "Прямоугольник")
                            figures.Add(new Figure(new Rectangle(pf), chart1));

                        figures.Last().Draw();
                        break;
                }

                comboBoxChoseMoveFigure.Items.Add(comboBoxAdd.SelectedItem.ToString() + ": " + figures.Last().shape.ToString());
                comboBoxDeleteFigure.Items.Add(comboBoxAdd.SelectedItem.ToString() + ": " + figures.Last().shape.ToString());
                comboBoxCrossFigure1.Items.Add(comboBoxAdd.SelectedItem.ToString() + ": " + figures.Last().shape.ToString());
                comboBoxCrossFigure2.Items.Add(comboBoxAdd.SelectedItem.ToString() + ": " + figures.Last().shape.ToString());

                MessageBox.Show("Добавление успешно завершено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла следующая ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAdd.SelectedItem.ToString() == "Окружность")
            {
                numericUpDownPointsCount.Value = 1;
                numericUpDownPointsCount.Enabled = false;
                return;
            }
            if (comboBoxAdd.SelectedItem.ToString() == "Отрезок")
            {
                numericUpDownPointsCount.Value = 2;
                numericUpDownPointsCount.Enabled = false;
                return;
            }
            if (comboBoxAdd.SelectedItem.ToString() == "Треугольник")
            {
                numericUpDownPointsCount.Value = 3;
                numericUpDownPointsCount.Enabled = false;
                return;
            }
            if (comboBoxAdd.SelectedItem.ToString() == "Четырёхугольник" || comboBoxAdd.SelectedItem.ToString() == "Прямоугольник" || comboBoxAdd.SelectedItem.ToString() == "Трапеция")
            {
                numericUpDownPointsCount.Value = 4;
                numericUpDownPointsCount.Enabled = false;
                return;
            }

            if (!numericUpDownPointsCount.Enabled) numericUpDownPointsCount.Enabled = true;
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxRes.Visible = false;
                labelRes.Visible = false;

                foreach (var figure in figures)
                {
                    if (chart1.Series[figure.number].Color == Color.Red)
                        chart1.Series[figure.number].Color = Color.Black;
                }

                int figure_number = comboBoxChoseMoveFigure.SelectedIndex;

                switch (comboBoxMovement.SelectedItem.ToString())
                {
                    case "Сдвиг":
                        figures[figure_number].Clear();
                        figures[figure_number].shape = figures[figure_number].shape.Shift(new Point2D(new double[] { double.Parse(textBoxShiftX.Text), double.Parse(textBoxShiftY.Text) }));
                        figures[figure_number].Draw();
                        break;

                    case "Поворот":
                        figures[figure_number].Clear();
                        figures[figure_number].shape = figures[figure_number].shape.Rot(double.Parse(textBoxRot.Text));
                        figures[figure_number].Draw();
                        break;

                    case "Симметрия":
                        figures[figure_number].Clear();

                        int axis = 0;
                        if (comboBoxSymAxis.SelectedItem.ToString() == "y")
                            axis = 1;

                        figures[figure_number].shape = figures[figure_number].shape.SymAxis(axis);
                        figures[figure_number].Draw();
                        break;
                }
                MessageBox.Show("Движение фигуры успешно завершено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла следующая ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxRes.Visible = false;
                labelRes.Visible = false;

                foreach (var figure in figures)
                {
                    if (chart1.Series[figure.number].Color == Color.Red)
                        chart1.Series[figure.number].Color = Color.Black;
                }

                textBoxRes.Visible = false;
                labelRes.Visible = false;

                int figure_number = comboBoxDeleteFigure.SelectedIndex;
                figures[figure_number].Clear();
                figures.RemoveAt(figure_number);
                comboBoxChoseMoveFigure.Items.RemoveAt(figure_number);
                comboBoxDeleteFigure.Items.RemoveAt(figure_number);
                comboBoxCrossFigure1.Items.RemoveAt(figure_number);
                comboBoxCrossFigure2.Items.RemoveAt(figure_number);

                MessageBox.Show("Удалеие фигуры успешно завершено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла следующая ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCross_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (var v in figures)
                {
                    if (chart1.Series[v.number].Color == Color.Red)
                        chart1.Series[v.number].Color = Color.Black;
                }

                textBoxRes.Clear();
                int figure_first_number = comboBoxCrossFigure1.SelectedIndex;
                int figure_second_number = comboBoxCrossFigure2.SelectedIndex;

                if (figures[figure_first_number].shape.Cross(figures[figure_second_number].shape))
                    textBoxRes.Text += "Пересекаются";
                else
                    textBoxRes.Text += "Не пересекаются";
                labelRes.Text = "Пересечение:";

                chart1.Series[figures[figure_first_number].number].Color = Color.Red;
                chart1.Series[figures[figure_second_number].number].Color = Color.Red;

                textBoxRes.Visible = true;
                labelRes.Visible = true;

                MessageBox.Show("Проверка фигур на пересечение успешно завершена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла следующая ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}