using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IRAVE.System_pages.Administration
{
    public partial class Admin_Dashboard : System.Web.UI.Page
    {
        StringBuilder htmlTable = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Login"] == null)
                {
                    Response.Redirect("~/System_pages/Administration/Log_In_form.aspx");
                }
                else {
                 

                    //body.Visible = false;
                    loader.Visible = true;
                    Div1.Visible = false;
                    mods.Visible = false;
                    //body.Visible = false;
                    adds.Visible = false;
                    Page.LoadComplete += new EventHandler(Page_LoadComplete);
                
                }

            }

        }
        void Page_LoadComplete(object sender, EventArgs e)
        {
            loader.Visible = false;
            admin();
            clients();

            if (Session["message"] == "1")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('Account created successfully! welcome!','Success!')", true);
                Session["login"] = null;
            }



            //body.Visible = true;

            //Label5.Text = "Success!";
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

        }


        protected void mod(object sender, EventArgs e)
        {
            mods.Visible = true;
            Div1.Visible = false;
            adds.Visible = false;
            information.Visible = false;
            serchresults.Visible = false;

        }
        protected void adding(object sender, EventArgs e)
        {
            mods.Visible = false;
            Div1.Visible = false;
            adds.Visible = true;
            information.Visible = false;




        }
        protected void clear(object sender, EventArgs e)
        {
            clear();




        }
        void clear()
        {
            mods.Visible = false;
            Div1.Visible = false;
            adds.Visible = false;
            information.Visible = true;
        }
        protected void del(object sender, EventArgs e)
        {
            mods.Visible = false;
            Div1.Visible = true;
            adds.Visible = false;
            information.Visible = false;
            deleteButton.Visible = false;


        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        protected void SearchDelete(object sender, EventArgs e)
        {
            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT *FROM Log_in WHERE Phone_number = '" + TextBox12.Text + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlCommand cmd1 = new SqlCommand(str, conn);

            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {

                deletSearch();
                deleteButton.Visible = true;
                mods.Visible = false;
                Div1.Visible = true;
                adds.Visible = false;
                information.Visible = false;


            }
            else
            {
                mods.Visible = false;
            Div1.Visible = true;
            adds.Visible = false;
            information.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('Error! A user with this number does not exists!','Error!')", true);

            }


        }
        protected void deleteCompletely(object sender, EventArgs e)
        {
            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "DELETE FROM Log_in WHERE Phone_number = '" + TextBox12.Text + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            GridView3.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('User removed from the system successfully','Removed')", true);
            deleteButton.Visible = false;
            mods.Visible = false;
            Div1.Visible = true;
            adds.Visible = false;
            information.Visible = false;

         }

        protected void submitnewusers(object sender, EventArgs e)
        {
            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT *FROM Log_in WHERE Phone_number = '" + TextBox5.Text + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('Error! A user with this number already exists!','Error!')", true);
                mods.Visible = false;
                Div1.Visible = false;
                adds.Visible = true;
                information.Visible = false;



            }
            else
            {
                insert();
                mods.Visible = false;
                Div1.Visible = false;
                adds.Visible = true;
                information.Visible = false;

            }


        }
        protected void test(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed()", true);

        }
        protected void Search1(object sender, EventArgs e)
        {
            serchresults.Visible = false;
            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT *FROM Log_in WHERE Phone_number = '" + TextBox11.Text + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                serchresults.Visible = true;
                //errormsg.Text = myDataReader["error_msg"].ToString();

                TextBox6.Text = myDataReader["First_name"].ToString();
                TextBox7.Text = myDataReader["Second_Name"].ToString();
                TextBox9.Text = myDataReader["Password"].ToString();
                TextBox3.Text = myDataReader["Phone_number"].ToString();

                mods.Visible = true;
                Div1.Visible = false;
                adds.Visible = false;
                information.Visible = false;

            }
            else
            {
                //errormsg.Text="data not found";
                mods.Visible = true;
                Div1.Visible = false;
                adds.Visible = false;
                information.Visible = false;
                serchresults.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('A user with this phone number does not exist please check and try again!!','Fail!')", true);

            }

            cmd.Connection.Close();
        }
        void admin()
        {
           
            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT COUNT(User_ID) AS ADMINS FROM Log_in WHERE Role_id='1'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                Label4.Text = myDataReader["ADMINS"].ToString();
            }
        }
        void clients()
        {

            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT COUNT(User_ID) AS ADMINS FROM Log_in where Role_id='2'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                Label5.Text = myDataReader["ADMINS"].ToString();
            }
        }
        void events()
        {

            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT COUNT(User_ID) AS ADMINS FROM Log_in";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                Label4.Text = myDataReader["ADMINS"].ToString();
            }
        }
        void online()
        {

            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT COUNT(User_ID) AS ADMINS FROM Log_in";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                //Label4.Text = myDataReader["ADMINS"].ToString();
            }

        }
        void deletSearch()
        {

            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT *FROM Log_in WHERE Phone_number = '" + TextBox12.Text + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "ss");

            GridView3.DataSource = ds.Tables["ss"]; ;
            GridView3.DataBind();
        }
        void insert()
        {
            string strcon = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ConnectionString;
            //create new sqlconnection and connection to database by using connection string from web.config file  
            SqlConnection con = new SqlConnection(strcon);
            {

                using (SqlCommand insertCommand = con.CreateCommand())
                {
                    Guid id = Guid.NewGuid();
                    DateTime today = DateTime.Today;
                    insertCommand.CommandText = "INSERT INTO Log_in(User_ID,First_name,Second_Name,Phone_number,Password,Role_id,Date_created) VALUES (@id,@fn,@sn,@pn,@ps,@rl,@dc)";
                    insertCommand.Parameters.AddWithValue("@id", id);
                    insertCommand.Parameters.AddWithValue("@fn", TextBox1.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@sn", TextBox2.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@pn", TextBox5.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@ps", TextBox4.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@dc", today.ToString());
                    insertCommand.Parameters.AddWithValue("@rl", DropDownList1.SelectedItem.Value);
                    insertCommand.Connection.Open();
                    insertCommand.ExecuteNonQuery();
                    con.Close();
                    information.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('Submitted successfully!!!!','Success!')", true);


                }
            }
        }
     
        void update()
        {
            string strcon = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ConnectionString;
            //create new sqlconnection and connection to database by using connection string from web.config file  
            SqlConnection con = new SqlConnection(strcon);
            {

                using (SqlCommand insertCommand = con.CreateCommand())
                {
                    Guid id = Guid.NewGuid();
                    DateTime today = DateTime.Today;
                    insertCommand.CommandText = "UPDATE Log_in SET First_name=@fn,Password=@ps,Second_Name=@sn,Phone_number=@pn,Role_id=@rl Where Phone_number=@sc";
                    insertCommand.Parameters.AddWithValue("@id", id);
                    insertCommand.Parameters.AddWithValue("@fn", TextBox6.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@sn", TextBox7.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@pn", TextBox3.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@sc", TextBox11.Text.ToString());

                    insertCommand.Parameters.AddWithValue("@ps", TextBox9.Text.ToString());
                    insertCommand.Parameters.AddWithValue("@dc", today.ToString());
                    insertCommand.Parameters.AddWithValue("@rl", DropDownList2.SelectedItem.Value);
                    insertCommand.Connection.Open();
                    insertCommand.ExecuteNonQuery();
                    con.Close();
                    information.Visible = true;
                    mods.Visible = true;
                    Div1.Visible = false;
                    adds.Visible = false;
                    information.Visible = false;
                    serchresults.Visible = false;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('Recods updated successfully!!','Success!')", true);



                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mods.Visible = true;
            Div1.Visible = false;
            adds.Visible = false;
            information.Visible = false;
            serchresults.Visible = false;
        }
        protected void updating(object sender, EventArgs e)
        {
            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT *FROM Log_in WHERE Phone_number = '" + TextBox11.Text + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                update();
                GridView1.DataBind();

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('Fail!','A user with this phone number does not exist please check and try again!!')", true);
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox11.Text = this.GridView2.SelectedRow.Cells[3].Text;


            serchresults.Visible = false;
            string strng = ConfigurationManager.ConnectionStrings["IraveConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(strng);
            conn.Open();
            string str = "SELECT *FROM Log_in WHERE Phone_number = '" + TextBox11.Text + "'";
            SqlCommand cmd = new SqlCommand(str, conn);
            SqlDataReader myDataReader = cmd.ExecuteReader();
            if (myDataReader.Read())
            {
                serchresults.Visible = true;
                //errormsg.Text = myDataReader["error_msg"].ToString();

                TextBox6.Text = myDataReader["First_name"].ToString();
                TextBox7.Text = myDataReader["Second_Name"].ToString();
                TextBox9.Text = myDataReader["Password"].ToString();
                TextBox3.Text = myDataReader["Phone_number"].ToString();

                mods.Visible = true;
                Div1.Visible = false;
                adds.Visible = false;
                information.Visible = false;
                serchresults.Visible = true;

            }
            else
            {
                //errormsg.Text="data not found";
                mods.Visible = true;
                Div1.Visible = false;
                adds.Visible = false;
                information.Visible = false;
                serchresults.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "confirmed('A user with this phone number does not exist please check and try again!!','Fail!')", true);

            }

            cmd.Connection.Close();
            //information.Visible = true;
            //mods.Visible = true;
            //Div1.Visible = false;
            //adds.Visible = false;
            //information.Visible = false;
            //serchresults.Visible = false;

        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/System_pages/Administration/Log_In_form.aspx");

        }

      

    }
}

