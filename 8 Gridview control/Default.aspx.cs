using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=D:\\ASP\\Unit 3\\Gridview control\\App_Data\\Database.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvdisplay();
        }
    }
    public void gvdisplay()
    {
        cmd = new SqlCommand("select * from student", con);
        con.Open();
       dr= cmd.ExecuteReader();
       GridView1.DataSource = dr;
       GridView1.DataBind();

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        gvdisplay();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label id = GridView1.Rows[e.RowIndex].FindControl("Label5") as Label;
        TextBox name=GridView1.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
        TextBox city = GridView1.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;
        TextBox pin = GridView1.Rows[e.RowIndex].FindControl("TextBox3") as TextBox;

        cmd = new SqlCommand("update student set name='"+name.Text+"',city='"+city.Text+"',pin="+pin.Text+" where id="+id.Text+"",con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        gvdisplay();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        gvdisplay();
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        TextBox id = GridView1.FooterRow.FindControl("TextBox4") as TextBox;
        TextBox name = GridView1.FooterRow.FindControl("TextBox5") as TextBox;
        TextBox city = GridView1.FooterRow.FindControl("TextBox6") as TextBox;
        TextBox pin = GridView1.FooterRow.FindControl("TextBox7") as TextBox;

        cmd=new SqlCommand("insert into student values("+id.Text+",'"+name.Text+"','"+city.Text+"',"+pin.Text+")",con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        gvdisplay();

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label id = GridView1.Rows[e.RowIndex].FindControl("Label1") as Label;
        cmd = new SqlCommand("delete from student where id="+id.Text+"",con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        gvdisplay();

    }
}
