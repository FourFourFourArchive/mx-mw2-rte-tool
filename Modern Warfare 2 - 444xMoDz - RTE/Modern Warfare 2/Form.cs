using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

#region Extensions + Enumerators
public enum MState : byte
{
    None = 0,
    Over = 1,
    Down = 2,
}
public enum CState : byte
{
    Horizontal = 0,
    Vertical = 1
}
public enum CColor : byte
{
    Black = 0,
    Blue = 1,
    Red = 2,
    Green = 3,
    Grey = 4,
    Purple = 5,
    Steel_Blue = 6,
    Yellow = 7,
    Magenta = 8,
    Orange = 9,
    Cyan = 10,
    AquaMarine = 11,
    LightGreen = 12,
    White = 13,
    DarkSlate = 14,
    Rose = 15,
    Chocolate = 16,
    Silver = 17,
    Gold = 18,
    The = 19,
    Turquoise = 20,
    Tomato = 21,
    Slate_Blue = 22,
    DarkTurquoise = 23,
    SkyBlue = 24,
    MidnightBlue = 25,
    DodgerBlue = 26,
    BlueViolet = 27,
    DarkGrey = 28,
    Plum = 29
}
public enum CStyle : byte
{
    Round = 0,
    Square = 1
}

[System.ComponentModel.DesignerCategory("Code")]
public static class Draw
{
    #region Grain Texture
    public static Bitmap GrainCode = (Bitmap)(System.Drawing.Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String(
        "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"))));
    #endregion
    public static TextureBrush Grain = new TextureBrush(GrainCode, WrapMode.Tile);

    public static Point[] Triangle(Point Location, Size Size)
    {
        return new Point[4] { Location, new Point(Location.X + Size.Width, Location.Y), new Point(Location.X + Size.Width / 2, Location.Y + Size.Height), Location };
    }

    public static GraphicsPath Round(Rectangle R, int Curve)
    {
        GraphicsPath P;
        if (Curve <= 0) { P = new GraphicsPath(); P.AddRectangle(R); return P; }

        int ARW = Curve * 2;
        P = new GraphicsPath();
        P.AddArc(new Rectangle(R.X, R.Y, ARW, ARW), -180, 90);
        P.AddArc(new Rectangle(R.Width - ARW + R.X, R.Y, ARW, ARW), -90, 90);
        P.AddArc(new Rectangle(R.Width - ARW + R.X, R.Height - ARW + R.Y, ARW, ARW), 0, 90);
        P.AddArc(new Rectangle(R.X, R.Height - ARW + R.Y, ARW, ARW), 90, 90);
        P.AddLine(new Point(R.X, R.Height - ARW + R.Y), new Point(R.X, Curve + R.Y));
        return P;
    }

    public static Size Measure(Control C, Font Font, string Text)
    {
        return Graphics.FromImage(new Bitmap(C.Width, C.Height)).MeasureString(Text, Font, C.Width).ToSize();
    }

    public static void Pixel(Graphics G, Color C, int X, int Y)
    {
        G.FillRectangle(new SolidBrush(C), X, Y, 1, 1);
    }

    public static void Gradient(Graphics G, Color C1, Color C2, int X, int Y, int Width, int Height, float Angle = 90f)
    {
        Rectangle R = new Rectangle(X, Y, Width, Height);
        G.FillRectangle(new LinearGradientBrush(R, C1, C2, Angle), R);
    }
    public static void Gradient(Graphics G, Color C1, Color C2, Rectangle R, float Angle = 90f)
    {
        G.FillRectangle(new LinearGradientBrush(R, C1, C2, Angle), R);
    }

    public static LinearGradientBrush Gradient(Color C1, Color C2, Rectangle R, float Angle = 90f)
    {
        return new LinearGradientBrush(R, C1, C2, Angle);
    }
    public static LinearGradientBrush Gradient(Color C1, Color C2, int X, int Y, int Width, int Height, float Angle = 90f)
    {
        Rectangle R = new Rectangle(X, Y, Width, Height);
        return new LinearGradientBrush(R, C1, C2, Angle);
    }

    public static void Radial(Graphics G, Color C1, Color C2, int X, int Y, int Width, int Height, float Angle = 90f)
    {
        Rectangle R = new Rectangle(X, Y, Width, Height);
        G.FillEllipse(new LinearGradientBrush(R, C1, C2, Angle), R);
    }
    public static void Radial(Graphics G, Color C1, Color C2, Rectangle R, float Angle = 90f)
    {
        G.FillEllipse(new LinearGradientBrush(R, C1, C2, Angle), R);
    }

    public static void Image(Graphics G, Image I, int X, int Y)
    {
        if (I == null) return;
        G.DrawImage(I, X, Y, I.Width, I.Height);
    }
    public static void Image(Graphics G, Image I, Point Location)
    {
        if (I == null) return;
        G.DrawImage(I, Location.X, Location.Y, I.Width, I.Height);
    }

    public static void Text(Graphics G, Brush B, int X, int Y, Font Font, string Text)
    {
        if (Text.Length == 0) return;
        G.DrawString(Text, Font, B, X, Y);
    }
    public static void Text(Graphics G, Brush B, Point P, Font Font, string Text)
    {
        if (Text.Length == 0) return;
        G.DrawString(Text, Font, B, P);
    }

    public static void Borders(Graphics G, Pen P, int X, int Y, int W, int H, int Offset = 0)
    {
        G.DrawRectangle(P, X + Offset, Y + Offset, (W - (Offset * 2)) - 1, (H - (Offset * 2) - 1));
    }
    public static void Borders(Graphics G, Pen P, Rectangle R, int Offset = 0)
    {
        G.DrawRectangle(P, R.X + Offset, R.Y + Offset, (R.Width - (Offset * 2)) - 1, (R.Height - (Offset * 2) - 1));
    }
}
#endregion

public class SpaceForm : Form
{
    public SpaceForm()
    {
        SetStyle(ControlStyles.UserPaint, true);
        DoubleBuffered = true;

        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Verdana", 8);
        Size = new Size(550, 400);
        ShowIcon = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(Draw.Grain, ClientRectangle);
        base.OnPaint(e);
    }
}

public class SpaceDivider : Panel
{
    #region Properties
    private CState DividerState_ = CState.Horizontal;
    public CState DividerState
    {
        get { return DividerState_; }
        set
        {
            int OldW = Size.Width, OldH = Size.Height;
            DividerState_ = value;
            Size = new Size(DividerState_ == CState.Horizontal ? OldH : 4, DividerState_ == CState.Horizontal ? 4 : OldW);
            Invalidate();
        }
    }
    #endregion

    public SpaceDivider()
    {
        SetStyle((ControlStyles)2050, true);
        DoubleBuffered = true;

        BackColor = Color.Transparent;
        BorderStyle = BorderStyle.None;
        Size = new Size(40, 4);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.DrawLine(new Pen(Color.FromArgb(200, 10, 10, 10)), 1, 1, DividerState_ == CState.Horizontal ? Width - 2 : 1, DividerState_ == CState.Horizontal ? 1 : Height - 2);
        e.Graphics.DrawLine(new Pen(Color.FromArgb(200, 40, 40, 40)), DividerState_ == CState.Horizontal ? 1 : 2, DividerState_ == CState.Horizontal ? 2 : 1, DividerState_ == CState.Horizontal ? Width - 2 : 2, DividerState_ == CState.Horizontal ? 2 : Height - 2);
        base.OnPaint(e);
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        base.SetBoundsCore(x, y, DividerState_ == CState.Horizontal ? width : 4, DividerState_ == CState.Horizontal ? 4 : height, specified);
    }
}

public class SpaceButton : Control
{
    private Color[] DefColors = new Color[30] { Color.FromArgb(150, 0, 0, 0), Color.FromArgb(100, 24, 66, 122), Color.FromArgb(80, 156, 0, 0), Color.FromArgb(80, 0, 128, 0), Color.FromArgb(80, 128, 128, 128), Color.FromArgb(80, 128, 0, 128),Color.FromArgb(80, 35,107,142),Color.FromArgb(80, 204,50,153),Color.FromArgb(80, 255,69,0),Color.FromArgb(80, 0,255,255),Color.FromArgb(80, 112,219,147),Color.FromArgb(80, 0,255,0),Color.FromArgb(80, 252,252,252),Color.FromArgb(80, 47,79,79),Color.FromArgb(80, 255,193,193),Color.FromArgb(80, 92,51,23),Color.FromArgb(80, 230,232,250),Color.FromArgb(80, 205, 127, 50),Color.FromArgb(80, 153, 204, 50),Color.FromArgb(80, 
173, 234, 23 ), Color.FromArgb(80, 255,99,71),Color.FromArgb(80, 0,127,255),Color.FromArgb(80, 0,134,139),Color.FromArgb(80, 135,206,235),Color.FromArgb(80, 25,25,112),Color.FromArgb(80, 16,78,139),Color.FromArgb(80, 138,43,226),Color.FromArgb(80, 34,34,34),Color.FromArgb(80, 221,160,221),Color.FromArgb(80, 128,0,0) };


    #region Properties
    [Browsable(false)]
    public override Color BackColor
    {
        get { return base.BackColor; }
        set { base.BackColor = value; }
    }

    [Browsable(false)]
    public override Color ForeColor
    {
        get { return base.ForeColor; }
        set { base.ForeColor = value; }
    }

    [Browsable(false)]
    public override Image BackgroundImage
    {
        get { return base.BackgroundImage; }
        set { base.BackgroundImage = value; }
    }

    [Browsable(false)]
    public override ImageLayout BackgroundImageLayout
    {
        get { return base.BackgroundImageLayout; }
        set { base.BackgroundImageLayout = value; }
    }

    [DefaultValue(typeof(Font), "Verdana, 8")]
    public override Font Font
    {
        get { return base.Font; }
        set { base.Font = value; }
    }

    private int ButtonCurve_ = 5;
    [Description("The curve in-which the button will draw it's edges.")]
    public int ButtonCurve
    {
        get { return ButtonCurve_; }
        set { ButtonCurve_ = value >= 0 ? value : 0; Invalidate(); }
    }

    private CColor ButtonColor_ = CColor.Purple;
    [Description("The color of the button ranging from ~\nBlack(0, 0, 0), Blue(24, 66, 122), and Red(156, 0, 0).")]
    public CColor ButtonColor
    {
        get { return ButtonColor_; }
        set { ButtonColor_ = value; Invalidate(); }
    }

    private CStyle ButtonStyle_ = CStyle.Round;
    [Description("This switch will override the 'ButtonCurve' property to allow, or disallow the changing of its value.")]
    public CStyle ButtonStyle
    {
        get { return ButtonStyle_; }
        set { ButtonStyle_ = value; ButtonCurve = value == CStyle.Square ? 0 : 5; Invalidate(); }
    }

    private MState MouseState = MState.None;
    protected override void OnMouseLeave(EventArgs e)
    {
        MouseState = MState.None;
        Invalidate();
    }
    protected override void OnMouseEnter(EventArgs e)
    {
        MouseState = MState.Over;
        Invalidate();
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            MouseState = MState.Over;
        Invalidate();
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            MouseState = MState.Down;
        Invalidate();
    }
    #endregion

    public SpaceButton()
    {
        SetStyle((ControlStyles)2050, true);
        DoubleBuffered = true;

        Font = new Font("Verdana", 8);
        Size = new Size(115, 23);
        BackColor = Color.Transparent;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Rectangle CR = new Rectangle(0, 0, Width - 1, Height - 1);
        Rectangle IR = new Rectangle(1, 1, Width - 3, Height - 3);
        Rectangle TR = new Rectangle(0, 0, Width, Height + 1);
        Rectangle TRS = new Rectangle(0, 0, Width + 2, Height + 2);
        Pen Outline = new Pen(Color.FromArgb((int)ButtonColor == 0 ? ((int)MouseState == 1 ? 13 : 11) : ((int)MouseState == 1 ? 18 : 14), Color.White));

        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        e.Graphics.FillPath(Draw.Grain, Draw.Round(CR, ButtonCurve));

        if ((int)ButtonColor == 0)
            e.Graphics.FillPath(Draw.Gradient(Color.FromArgb((int)MouseState == 2 ? 100 : 50, Color.Black), Color.FromArgb((int)MouseState == 2 ? 70 : 100, Color.Black), CR, 90f), Draw.Round(CR, ButtonCurve));
        else
        {
            e.Graphics.FillPath(new SolidBrush(DefColors[(int)ButtonColor]), Draw.Round(CR, ButtonCurve));
            e.Graphics.FillPath(Draw.Gradient((int)MouseState == 2 ? Color.FromArgb(50, Color.Black) : Color.Transparent, (int)MouseState == 2 ? Color.Transparent : Color.FromArgb(50, Color.Black), CR, 90f), Draw.Round(CR, ButtonCurve));
        }

        e.Graphics.DrawPath(new Pen(Color.FromArgb(250, 15, 15, 15)), Draw.Round(CR, ButtonCurve));
        e.Graphics.DrawPath(Outline, Draw.Round(IR, ButtonCurve));

        e.Graphics.DrawString(Text, Font, new SolidBrush(Color.FromArgb(120, Color.Black)), TRS, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        e.Graphics.DrawString(Text, Font, new SolidBrush(Color.FromArgb(220, 225, 225, 225)), TR, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

        if (!Enabled)
            e.Graphics.FillPath(new SolidBrush(Color.FromArgb(100, Color.Black)), Draw.Round(CR, ButtonCurve));

        base.OnPaint(e);
    }

    protected override void OnTextChanged(EventArgs e)
    {
        Invalidate();
        base.OnTextChanged(e);
    }
}

[DefaultEvent("CheckedChanged")]
public class SpaceCheckBox : Control
{
    private Color[] DefColors = new Color[3] { Color.FromArgb(0, 0, 0), Color.FromArgb(24, 66, 122), Color.FromArgb(156, 0, 0) };

    #region Events
    public event CheckedChangedEventHandler CheckedChanged;
    public delegate void CheckedChangedEventHandler(object sender, bool isChecked);
    #endregion

    #region Properties
    private CColor CheckColor_ = CColor.Blue;
    [Description("The color of the check mark ranging from ~\nBlack(0, 0, 0) Blue(24, 66, 122), and Red(156, 0, 0).")]
    public CColor CheckColor
    {
        get { return CheckColor_; }
        set { CheckColor_ = value; Invalidate(); }
    }

    private bool Checked_;
    [Description("Indicates whether the component is in the checked state.")]
    public bool Checked
    {
        get { return Checked_; }
        set { Checked_ = value; Invalidate(); if (CheckedChanged != null)CheckedChanged(this, value); }
    }

    private MState MouseState = MState.None;
    protected override void OnMouseLeave(EventArgs e)
    {
        MouseState = MState.None;
        Invalidate();
    }
    protected override void OnMouseEnter(EventArgs e)
    {
        MouseState = MState.Over;
        Invalidate();
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            MouseState = MState.Over;
        Invalidate();
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            MouseState = MState.Down;
        Invalidate();
    }
    #endregion

    public SpaceCheckBox()
    {
        SetStyle((ControlStyles)2050, true);
        DoubleBuffered = true;

        BackColor = Color.Transparent;
        ForeColor = Color.FromArgb(220, 225, 225, 225);
        Font = new Font("Verdana", 8);
        Size = new Size(135, 16);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        StringFormat SF = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        Rectangle CB = new Rectangle(0, 0, 16, 16);
        Rectangle IR = new Rectangle(1, 1, 13, 13);

        e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
        e.Graphics.FillRectangle(Draw.Gradient(Color.FromArgb(100, Color.Black), Color.Transparent, IR, 45f), IR);
        Draw.Borders(e.Graphics, new Pen(Color.FromArgb(250, 15, 15, 15)), CB, 0);
        Draw.Borders(e.Graphics, new Pen(Color.FromArgb((int)MouseState == 1 ? 10 : 8, Color.White)), CB, 1);
        Draw.Borders(e.Graphics, new Pen(Color.FromArgb(150, 15, 15, 15)), CB, 2);

        if (Checked)
            e.Graphics.DrawString("√", new Font("Verdana", 8, FontStyle.Bold), new SolidBrush(DefColors[(int)CheckColor]), 1, 1);

        e.Graphics.DrawString(Text, Font, new SolidBrush(Color.FromArgb(100, Color.Black)), 20, 2);
        e.Graphics.DrawString(Text, Font, new SolidBrush(Color.FromArgb(220, 225, 225, 225)), 18, 1);

        base.OnPaint(e);
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        base.SetBoundsCore(x, y, 22 + (int)CreateGraphics().MeasureString(Text, Font).Width, 16, specified);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            Checked = !Checked;
        base.OnMouseClick(e);
    }

    protected override void OnTextChanged(EventArgs e)
    {
        Width = 22 + (int)CreateGraphics().MeasureString(Text, Font).Width;
        base.OnTextChanged(e);
    }
}

[DefaultEvent("TextChanged")]
public class SpaceTextBox : Control
{
    #region Properties
    private string[] Lines_;
    public string[] Lines
    {
        get { return Base != null ? Base.Lines : new string[] { "<Empty>" }; }
        set { if (Base != null)Base.Lines = value; }
    }

    private int Curve_ = 1;
    public int Curve
    {
        get { return Curve_; }
        set { Curve_ = value >= 0 ? value : 1; Invalidate(); }
    }

    private int LockHeight_;
    private int LockHeight
    {
        get { return LockHeight_; }
        set
        {
            LockHeight_ = value;
            if (!(LockHeight == 0) && IsHandleCreated)
                Height = LockHeight;
        }
    }

    private bool ReadOnly_ = false;
    public bool ReadOnly
    {
        get { return ReadOnly_; }
        set { ReadOnly_ = value; if (Base != null)Base.ReadOnly = value; Invalidate(); }
    }

    private bool Multiline_ = false;
    public bool Multiline
    {
        get { return Multiline_; }
        set
        {
            Multiline_ = value;
            if (Base != null)
            {
                Base.Multiline = value;
                if (value) { LockHeight = 0; Base.Height = Height - 7; }
                else { LockHeight = Base.Height + 7; }
            }
            Invalidate();
        }
    }

    private int MaxLength_ = int.MaxValue;
    public int MaxLength
    {
        get { return MaxLength_; }
        set { MaxLength_ = value; if (Base != null)Base.MaxLength = value; Invalidate(); }
    }

    private bool UseSystemPasswordChar_ = false;
    public bool UseSystemPasswordChar
    {
        get { return UseSystemPasswordChar_; }
        set { UseSystemPasswordChar_ = value; if (Base != null)Base.UseSystemPasswordChar = value; Invalidate(); }
    }

    private HorizontalAlignment TextAlign_ = HorizontalAlignment.Left;
    public HorizontalAlignment TextAlign
    {
        get { return TextAlign_; }
        set { TextAlign_ = value; if (Base != null)Base.TextAlign = value; Invalidate(); }
    }

    public override Font Font
    {
        get { return base.Font; }
        set { base.Font = value; if (Base != null) Base.Font = value; Invalidate(); }
    }
    public override string Text
    {
        get { return Base != null ? Base.Text : ""; }
        set { base.Text = value; if (Base != null)Base.Text = value; Invalidate(); }
    }
    public override Cursor Cursor
    {
        get { return Cursors.IBeam; }
        set { base.Cursor = Cursors.IBeam; }
    }
    #endregion

    #region Functions
    public void ScrollToCaret()
    { if (Base != null)Base.ScrollToCaret(); }
    #endregion

    public TextBox Base;
    public SpaceTextBox()
    {
        Base = new TextBox();
        SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
        BackColor = Color.Transparent;
        Font = new Font("Verdana", 8);
        #region Base Properties
        Base.BackColor = Color.FromArgb(22, 22, 22);
        Base.ForeColor = Color.FromArgb(225, 225, 225);
        Base.Font = Font;
        Base.Text = Text;
        Base.MaxLength = MaxLength_;
        Base.Multiline = Multiline_;
        Base.ReadOnly = ReadOnly_;
        Base.TextAlign = TextAlign_;
        Base.UseSystemPasswordChar = UseSystemPasswordChar_;
        Base.BorderStyle = BorderStyle.None;
        Base.Location = new Point(5, 3);
        Base.Width = Width - 10;
        ClientSize = new Size(156, 16);
        if (Multiline) Base.Height = Height - 7;
        else LockHeight = Base.Height + 7;
        #endregion
        Invalidate();
    }

    protected override void OnCreateControl()
    {
        if (!Controls.Contains(Base)) Controls.Add(Base);
        base.OnCreateControl();
    }

    protected override void OnResize(EventArgs e)
    {
        Base.Location = new Point(5, 3);
        Base.Width = Width - 10;
        if (Multiline) Base.Height = Height - 7;
        else LockHeight = Base.Height + 7;
        base.OnResize(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Rectangle CR = new Rectangle(0, 0, Width - 1, Height - 1);
        Rectangle IR = new Rectangle(1, 1, Width - 3, Height - 3);
        Rectangle IIR = new Rectangle(2, 2, Width - 5, Height - 5);
        Bitmap B = new Bitmap(Width, Height);
        Graphics G = Graphics.FromImage(B);

        G.Clear(BackColor);
        G.SmoothingMode = SmoothingMode.HighQuality;

        G.FillPath(new SolidBrush(Base.BackColor), Draw.Round(CR, Curve));
        G.DrawPath(new Pen(Color.FromArgb(250, 15, 15, 15)), Draw.Round(CR, Curve));
        G.DrawPath(new Pen(Color.FromArgb(4, Color.White)), Draw.Round(IR, Curve));

        e.Graphics.DrawImage(B, 0, 0);
        G.Dispose(); B.Dispose();
        base.OnPaint(e);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        if (!(LockHeight_ == 0))
            Height = LockHeight_;
        base.OnHandleCreated(e);
    }

    protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
    {
        if (!(LockHeight_ == 0))
            height = LockHeight_;
        base.SetBoundsCore(x, y, width, height, specified);
    }
}

public class SpaceComboBox : ComboBox
{
    #region Properties
    private CStyle ControlStyle_;
    public CStyle ControlStyle
    {
        get { return ControlStyle_; }
        set { ControlStyle_ = value; Invalidate(); }
    }

    public ComboBoxStyle DropDownStyle
    {
        get { return System.Windows.Forms.ComboBoxStyle.DropDownList; }
        set { base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; }
    }
    public DrawMode DrawMode
    {
        get { return DrawMode.OwnerDrawFixed; }
        set { base.DrawMode = DrawMode.OwnerDrawFixed; }
    }

    private MState CursorState_ = MState.None;
    protected override void OnMouseLeave(EventArgs e)
    {
        CursorState_ = MState.None;
        Invalidate();
        base.OnMouseLeave(e);
    }
    protected override void OnMouseEnter(EventArgs e)
    {
        CursorState_ = MState.Over;
        Invalidate();
        base.OnMouseEnter(e);
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            CursorState_ = MState.Over;
        Invalidate();
        base.OnMouseUp(e);
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            CursorState_ = MState.Down;
        Invalidate();
        base.OnMouseDown(e);
    }
    #endregion

    public SpaceComboBox()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        ControlStyle = CStyle.Round;
        ForeColor = Color.FromArgb(225, 225, 225);
        Font = new Font("Verdana", 8);
        BackColor = Color.Transparent;
        ItemHeight = 17;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Bitmap B = new Bitmap(Width, Height);
        Graphics G = Graphics.FromImage(B);

        int SH = (int)G.MeasureString("...", Font).Height;
        bool isOver = CursorState_ == MState.Over ? true : false;
        bool isDown = CursorState_ == MState.Down ? true : false;
        bool isRound = ControlStyle == CStyle.Round ? true : false;

        Rectangle CR = new Rectangle(0, 0, Width - 1, Height - 1);
        Rectangle IR = new Rectangle(1, 1, Width - 3, Height - 3);
        Rectangle RB = new Rectangle(Width - 23, 1, 21, Height - 3);
        Rectangle RO = new Rectangle(Width - 24, 1, 22, Height - 3);
        LinearGradientBrush Gradient = Draw.Gradient(Color.FromArgb(50, Color.Black), Color.FromArgb(100, Color.Black), CR, 90f);
        G.Clear(BackColor);
        G.SmoothingMode = SmoothingMode.HighQuality;

        G.FillPath(Draw.Grain, Draw.Round(CR, isRound ? 5 : 0));
        G.FillPath(Gradient, Draw.Round(CR, isRound ? 5 : 0));
        G.DrawPath(new Pen(Color.FromArgb(250, 15, 15, 15)), Draw.Round(CR, isRound ? 5 : 0));
        G.DrawPath(new Pen(Color.FromArgb(isOver ? 11 : 9, Color.White)), Draw.Round(IR, isRound ? 5 : 0));

        G.DrawLine(new Pen(Color.FromArgb(isOver ? 11 : 9, Color.White)), Width - 26, 2, Width - 26, Height - 3);
        G.DrawLine(new Pen(Color.FromArgb(250, 15, 15, 15)), Width - 27, 2, Width - 27, Height - 3);

        G.DrawString(SelectedIndex != -1 ? " " + Items[SelectedIndex].ToString() : Items != null && Items.Count > 0 ? " " + Items[0].ToString() : " <Empty>", Font, new SolidBrush(Color.FromArgb(120, Color.Black)), 5, ((Height / 2) - ((SH + 1) / 2)) + 1);
        G.DrawString(SelectedIndex != -1 ? " " + Items[SelectedIndex].ToString() : Items != null && Items.Count > 0 ? " " + Items[0].ToString() : " <Empty>", Font, new SolidBrush(Color.FromArgb(200, 225, 225, 225)), 4, (Height / 2) - ((SH + 1) / 2));

        G.FillPolygon(new SolidBrush(Color.FromArgb(120, Color.Black)), Draw.Triangle(new Point(Width - 17, Height / 2), new Size(7, 3)));
        G.FillPolygon(new SolidBrush(Color.FromArgb(isOver ? 240 : 220, 225, 225, 225)), Draw.Triangle(new Point(Width - 18, (Height / 2) - 1), new Size(7, 3)));

        e.Graphics.DrawImage(B, 0, 0);
        G.Dispose(); B.Dispose();
        base.OnPaint(e);
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        bool isRound = ControlStyle == CStyle.Round ? true : false;
        if (e.Index < 0) return;

        if ((int)e.State == 785 | (int)e.State == 17 | e.State == DrawItemState.Focus | e.State == DrawItemState.HotLight | e.State == DrawItemState.Selected)
        {
            Rectangle SelRec = new Rectangle(new Point(e.Bounds.X, e.Bounds.Y), new Size(e.Bounds.Width - 1, e.Bounds.Height - 1));
            e.Graphics.FillRectangle(Draw.Grain, e.Bounds);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Black)), e.Bounds);

            e.Graphics.FillPath(new SolidBrush(Color.FromArgb(120, Color.Black)), Draw.Round(SelRec, isRound ? 4 : 0));
            e.Graphics.DrawPath(new Pen(Color.FromArgb(40, 50, 50, 50)), Draw.Round(SelRec, isRound ? 4 : 0));
            e.Graphics.DrawString("  " + Items[e.Index].ToString(), Font, new SolidBrush(Color.FromArgb(225, 225, 225)), e.Bounds.X, e.Bounds.Y + 1);
        }
        else
        {
            e.Graphics.FillRectangle(Draw.Grain, e.Bounds);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Black)), e.Bounds);
            e.Graphics.DrawString(" " + Items[e.Index].ToString(), Font, new SolidBrush(Color.FromArgb(225, 225, 225)), e.Bounds.X, e.Bounds.Y + 1);
        }
        base.OnDrawItem(e);
    }
}

public class SpaceTabControl : TabControl
{
    private Color TabHighlight_ = Color.FromArgb(36, 75, 140);
    [DefaultValue(typeof(Color), "36, 75, 140")]
    public Color TabHighlight
    {
        get { return TabHighlight_; }
        set { TabHighlight_ = value.A == 255 ? value : Color.FromArgb(value.R, value.G, value.B); Invalidate(); }
    }

    public SpaceTabControl()
    {
        SetStyle((ControlStyles)2050, true);
        DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        StringFormat SF = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        e.Graphics.FillRectangle(Draw.Grain, ClientRectangle);
        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Black)), new Rectangle(new Point(0, 0), new Size(Width, 21)));

        Draw.Borders(e.Graphics, new Pen(Color.FromArgb(250, 15, 15, 15)), new Rectangle(new Point(0, 0), new Size(Width, 22)), 0);
        Draw.Borders(e.Graphics, new Pen(Color.FromArgb(250, 15, 15, 15)), new Rectangle(new Point(0, 21), new Size(Width, Height - 21)), 0);
        Draw.Borders(e.Graphics, new Pen(Color.FromArgb(9, Color.White)), new Rectangle(new Point(0, 21), new Size(Width, Height - 21)), 1);

        Rectangle TR;
        foreach (TabPage TP in TabPages)
        {
            if (TP.BackgroundImage != Draw.GrainCode) TP.BackgroundImage = Draw.GrainCode;
            if (TP.BackColor != Color.Black) TP.BackColor = Color.Black;
            TR = GetTabRect(TabPages.IndexOf(TP));

            if (TabPages.IndexOf(TP) == SelectedIndex)
            {
                e.Graphics.FillRectangle(Draw.Grain, new Rectangle(TR.X - 1, TR.Y - 1, TR.Width + 1, TR.Height - 1));
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Black)), new Rectangle(TR.X - 1, TR.Y - 1, TR.Width + 1, TR.Height - 1));
                Draw.Borders(e.Graphics, new Pen(Color.FromArgb(12, Color.FromArgb(15, 15, 15))), new Rectangle(TR.X - 1, TR.Y - 1, TR.Width + 1, TR.Height - 1));

                switch (SelectedIndex < 1)
                {
                    case true:
                        Draw.Gradient(e.Graphics, Color.Transparent, Color.FromArgb(255, TabHighlight), TR.Width + 1, 1, 1, (TR.Height / 2) - 1);
                        Draw.Gradient(e.Graphics, Color.FromArgb(255, TabHighlight), Color.Transparent, TR.Width + 1, TR.Height / 2, 1, (TR.Height / 2));
                        break;
                    case false:
                        Draw.Gradient(e.Graphics, Color.Transparent, Color.FromArgb(255, TabHighlight), new Rectangle(new Point(TR.X - 1, 1), new Size(1, (TR.Height / 2) - 1)));
                        Draw.Gradient(e.Graphics, Color.FromArgb(255, TabHighlight), Color.Transparent, new Rectangle(new Point(TR.X - 1, TR.Height / 2), new Size(1, TR.Height / 2)));
                        if (SelectedIndex == TabPages.Count - 1) break;
                        Draw.Gradient(e.Graphics, Color.Transparent, Color.FromArgb(255, TabHighlight), new Rectangle(new Point((TR.X + TR.Width) - 1, 1), new Size(1, (TR.Height / 2) - 1)));
                        Draw.Gradient(e.Graphics, Color.FromArgb(255, TabHighlight), Color.Transparent, new Rectangle(new Point((TR.X + TR.Width) - 1, TR.Height / 2), new Size(1, TR.Height / 2)));
                        break;
                }
            }
            if (TabPages.IndexOf(TP) == SelectedIndex)
                TR = new Rectangle(TR.X, TR.Y, TR.Width, TR.Height - 2);
            e.Graphics.DrawString(TP.Text, Font, new SolidBrush(Color.FromArgb(120, Color.Black)), TR, SF);
            e.Graphics.DrawString(TP.Text, Font, new SolidBrush(Color.FromArgb(220, 225, 225, 225)), TR, SF);
        }
    }
}