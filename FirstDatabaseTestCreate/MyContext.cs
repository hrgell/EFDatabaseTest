using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FirstDatabaseTestCreate.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace FirstDatabaseTestCreate
{
    // Create database contexts.
    // The class was added to support migrations (code first incremental design).
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Util.WriteLine("DesignTimeDbContextFactory.CreateDbContext called");
            string cs = Util.GetConnectionString();
            var builder = new DbContextOptionsBuilder<MyContext>();
            builder.UseSqlServer(cs);
            return new MyContext(builder.Options);
        }
    }

    public class MyContext : DbContext
    {
        // The tables
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<QryReplyOverview> ReplyOverview { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        // Initializer
        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }

        // Setup connection strings, etc.
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //Util.WriteLine("MyContext.OnConfiguring called");
            string cs = Util.GetConnectionString();
            builder.UseSqlServer(cs);
        }

        protected void SetupDefaults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCode>().Property(e => e.Disabled).HasDefaultValue(0);
            modelBuilder.Entity<PostalCode>().Property(e => e.Province).HasDefaultValue("");
            modelBuilder.Entity<PostalCode>().Property(e => e.City).HasDefaultValue("");
            modelBuilder.Entity<PostalCode>().Property(e => e.Zipcode).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Disabled).HasDefaultValue(0);
            modelBuilder.Entity<User>().Property(e => e.Name).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Title).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Gender).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Pwd).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.SecondaryEmail).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Phone).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.SecondaryPhone).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Homepage).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Notes).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Adr1).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Adr2).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Adr3).HasDefaultValue("");
            modelBuilder.Entity<User>().Property(e => e.Adr4).HasDefaultValue("");
            modelBuilder.Entity<Questionnaire>().Property(e => e.Disabled).HasDefaultValue(0);
            modelBuilder.Entity<Questionnaire>().Property(e => e.Visible).HasDefaultValue(1);
            modelBuilder.Entity<Survey>().Property(e => e.Visible).HasDefaultValue(1);
            modelBuilder.Entity<Question>().Property(e => e.Disabled).HasDefaultValue(0);
            modelBuilder.Entity<Question>().Property(e => e.Title).HasDefaultValue("");
            modelBuilder.Entity<Question>().Property(e => e.Notes).HasDefaultValue("");
            modelBuilder.Entity<Question>().Property(e => e.Forced).HasDefaultValue(0);
            modelBuilder.Entity<Question>().Property(e => e.Visible).HasDefaultValue(1);
            modelBuilder.Entity<Question>().Property(e => e.ScaleLimit).HasDefaultValue(5);
            modelBuilder.Entity<Question>().Property(e => e.TitleMax).HasDefaultValue("");
            modelBuilder.Entity<Question>().Property(e => e.TitleMin).HasDefaultValue("");
            modelBuilder.Entity<Question>().Property(e => e.TitleOther).HasDefaultValue("");
            modelBuilder.Entity<Question>().Property(e => e.DefaultValue).HasDefaultValue("");
            modelBuilder.Entity<Question>().Property(e => e.InputOrder).HasDefaultValue(0);
            modelBuilder.Entity<Question>().Property(e => e.Compact).HasDefaultValue(0);
            modelBuilder.Entity<Question>().Property(e => e.QuestionTitleWidth).HasDefaultValue("");
            modelBuilder.Entity<Question>().Property(e => e.AnswerTitleWidth).HasDefaultValue("");
            modelBuilder.Entity<Answer>().Property(e => e.Disabled).HasDefaultValue(0);
            modelBuilder.Entity<Answer>().Property(e => e.Visible).HasDefaultValue(1);
        } // SetupDefaults()

        protected void SetupIndexes(ModelBuilder modelBuilder)
        {
            // Indexes
            modelBuilder.Entity<PostalCode>().HasIndex(e => new { e.CountryCode, e.Zipcode, e.City }).IsUnique();
            modelBuilder.Entity<PostalCode>().HasIndex(e => new { e.Zipcode, e.City });
            modelBuilder.Entity<QuestionType>().HasIndex(e => e.QType).IsUnique();
            modelBuilder.Entity<ReplyType>().HasIndex(e => e.RType).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(e => e.PostalCodeId);
            modelBuilder.Entity<Questionnaire>().HasIndex(e => e.Title);
            modelBuilder.Entity<Survey>().HasIndex(e => e.QuestionnaireId);
            modelBuilder.Entity<Survey>().HasIndex(e => e.DateBegin);
            modelBuilder.Entity<Survey>().HasIndex(e => e.Title);
            modelBuilder.Entity<Reply>().HasIndex(e => e.UserId);
            modelBuilder.Entity<Reply>().HasIndex(e => e.SurveyId);
            modelBuilder.Entity<Reply>().HasIndex(e => e.AnswerId);

            //modelBuilder.Entity<Post>().HasOne(p => p.Blog).WithMany(b => b.Posts).HasForeignKey(x => x.BlogId);
            //modelBuilder.Entity<Questionnaire>().HasMany(w => w.Surveys).WithOne(e => e.Questionnaire).HasForeignKey(e => e.QuestionnaireId).OnDelete(DeleteBehavior.Restrict);

            // Dont delete replies and surveys if we delete the user.
            modelBuilder.Entity<User>().HasMany(w => w.Surveys).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(w => w.Replies).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
        } // SetupIndexes()

        protected void SetupData(ModelBuilder modelBuilder)
        {
            // Data
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { PostalCodeId = 1, CountryCode = "DK", Zipcode = "1000", City = "Københavh K" });
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { PostalCodeId = 2, CountryCode = "DK", Zipcode = "2000", City = "Frederiksberg C" });
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { PostalCodeId = 3, CountryCode = "DK", Zipcode = "2100", City = "Københavh Ø" });
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { PostalCodeId = 4, CountryCode = "DK", Zipcode = "2200", City = "Københavh N" });
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { PostalCodeId = 5, CountryCode = "DK", Zipcode = "2300", City = "Københavh S" });
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { PostalCodeId = 6, CountryCode = "DK", Zipcode = "2400", City = "Københavh NV" });
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { PostalCodeId = 7, CountryCode = "DK", Zipcode = "2500", City = "Valby" });

            modelBuilder.Entity<ReplyType>().HasData(new ReplyType { ReplyTypeId = 1, RType = 0, Title = "Normal" });
            modelBuilder.Entity<ReplyType>().HasData(new ReplyType { ReplyTypeId = 2, RType = 1, Title = "Other Choise" });
            modelBuilder.Entity<ReplyType>().HasData(new ReplyType { ReplyTypeId = 3, RType = 2, Title = "Other Text" });

            modelBuilder.Entity<QuestionType>().HasData(new QuestionType { QuestionTypeId = 1, QType = 0, Title = "None" });
            modelBuilder.Entity<QuestionType>().HasData(new QuestionType { QuestionTypeId = 2, QType = 1, Title = "Text" });
            modelBuilder.Entity<QuestionType>().HasData(new QuestionType { QuestionTypeId = 3, QType = 2, Title = "Single Choice" });
            modelBuilder.Entity<QuestionType>().HasData(new QuestionType { QuestionTypeId = 4, QType = 3, Title = "Multi Choice" });
            modelBuilder.Entity<QuestionType>().HasData(new QuestionType { QuestionTypeId = 5, QType = 4, Title = "Scale" });

            modelBuilder.Entity<User>().HasData(new User { UserId = 1, Name = "Hr Gell", Email = "hrgell@hotmail.com", PostalCodeId = 1 });

            modelBuilder.Entity<Questionnaire>().HasData(new Questionnaire { QuestionnaireId = 1, UserId = 1, Title = "Questionnaire 1", Weight = 5 });
            modelBuilder.Entity<Questionnaire>().HasData(new Questionnaire { QuestionnaireId = 2, UserId = 1, Title = "Questionnaire 2", Weight = 5 });

            modelBuilder.Entity<Question>().HasData(new Question { QuestionId = 1, QuestionnaireId = 1, Title = "Question 1 Kort tekst", Weight = 1, QType = 0, DisplayOrder = 1, QuestionTitleWidth = "300px" });
            modelBuilder.Entity<Question>().HasData(new Question { QuestionId = 2, QuestionnaireId = 1, Title = "Question 2 Lang tekst lang tekst lang tekst lang", Weight = 1, QType = 1, DisplayOrder = 2, AnswerTitleWidth = "300px", QuestionTitleWidth = "300px" });
            modelBuilder.Entity<Question>().HasData(new Question { QuestionId = 3, QuestionnaireId = 1, Title = "Question 3", Weight = 1, QType = 2, DisplayOrder = 3, AnswerTitleWidth = "300px", QuestionTitleWidth = "300px" });
            modelBuilder.Entity<Question>().HasData(new Question { QuestionId = 4, QuestionnaireId = 1, Title = "Question 4", Weight = 1, QType = 3, DisplayOrder = 4 });
            modelBuilder.Entity<Question>().HasData(new Question { QuestionId = 5, QuestionnaireId = 1, Title = "Question 5", TitleMax = "Good", TitleMin = "Bad", TitleOther = "Other", Weight = 1, QType = 4, DisplayOrder = 5 });
            modelBuilder.Entity<Question>().HasData(new Question { QuestionId = 6, QuestionnaireId = 1, Title = "Question 6", Weight = 1, QType = 0, DisplayOrder = 6 });

            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 1, Title = "Answer 2.1 Kort tekst", QuestionId = 2, DisplayOrder = 1 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 2, Title = "Answer 2.2 Lang tekst lang tekst lang tekst lang tekst", QuestionId = 2, DisplayOrder = 2 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 3, Title = "Answer 3.1", QuestionId = 3, DisplayOrder = 1 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 4, Title = "Answer 3.2 Lang tekst lang tekst lang tekst lang.", QuestionId = 3, DisplayOrder = 2 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 5, Title = "Answer 3.3 Kort tekst", QuestionId = 3, DisplayOrder = 3 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 6, Title = "Answer 4.1 Middel tekst som er kort", QuestionId = 4, DisplayOrder = 1 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 7, Title = "Answer 4.2", QuestionId = 4, DisplayOrder = 2 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 8, Title = "Answer 4.3 Kort tekst", QuestionId = 4, DisplayOrder = 3 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 9, Title = "Answer 4.4 Lang tekst lang tekst lang tekst lang.", QuestionId = 4, DisplayOrder = 4 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 10, Title = "Answer 5.1", QuestionId = 5, DisplayOrder = 1, Weight = 5 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 11, Title = "Answer 1.1 Kort tekst", QuestionId = 1, DisplayOrder = 1 });
            modelBuilder.Entity<Answer>().HasData(new Answer { AnswerId = 12, Title = "Answer 1.2 Lang tekst lang tekst lang tekst lang.", QuestionId = 1, DisplayOrder = 2 });

            var now = DateTime.Now;
            modelBuilder.Entity<Survey>().HasData(new Survey { SurveyId = 1, QuestionnaireId = 1, Visible = 0, Title = "#System", DateBegin = now.Add(new TimeSpan(4, 0, 0, 0)), DateEnd = now.Add(new TimeSpan(4, 4, 0, 0)), UserId = 1 });
            modelBuilder.Entity<Survey>().HasData(new Survey { SurveyId = 2, QuestionnaireId = 1, Visible = 1, Title = "Survey 2", DateBegin = now.Add(new TimeSpan(4, 0, 0, 0)), DateEnd = now.Add(new TimeSpan(4, 4, 0, 0)), UserId = 1 });
            modelBuilder.Entity<Survey>().HasData(new Survey { SurveyId = 3, QuestionnaireId = 2, Visible = 0, Title = "#System", DateBegin = now.Add(new TimeSpan(4, 0, 0, 0)), DateEnd = now.Add(new TimeSpan(0, 4, 0, 0)), UserId = 1 });

            modelBuilder.Entity<Reply>().HasData(new Reply { ReplyId = 1, SurveyId = 1, UserId = 1, AnswerId = 4, RType = 0, Value = "Reply to question 3" });
            modelBuilder.Entity<Reply>().HasData(new Reply { ReplyId = 2, SurveyId = 1, UserId = 1, AnswerId = 7, RType = 0, Value = "Reply to question 4.2" });
            modelBuilder.Entity<Reply>().HasData(new Reply { ReplyId = 3, SurveyId = 1, UserId = 1, AnswerId = 10, RType = 0, Value = "3" });
            modelBuilder.Entity<Reply>().HasData(new Reply { ReplyId = 4, SurveyId = 1, UserId = 1, AnswerId = 2, RType = 0, Value = "Lidt tekst" });

        } // SetupData()

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Util.WriteLine("MyContext.OnModelCreating called");
            // Do NOT create these tables
            modelBuilder.Ignore<QryReplyOverview>();
            modelBuilder.Entity<QryReplyOverview>().HasNoKey().ToView(null);
            modelBuilder.Ignore<QrySurveyOverview>();
            modelBuilder.Entity<QrySurveyOverview>().HasNoKey().ToView(null);
            modelBuilder.Ignore<QryReply>();
            modelBuilder.Entity <QryReply>().HasNoKey().ToView(null);
            // Configure defaults, indexes and initial data
            SetupDefaults(modelBuilder);
            SetupIndexes(modelBuilder);
            SetupData(modelBuilder);
        } // method OnModelCreating()
    } // class

    //public class MyContext2 : MyContext
    //{
    //    public MyContext2() : base(new DbContextOptionsBuilder<MyContext>().Options) { }
    //    public MyContext2(DbContextOptions<MyContext> options) : base(options) { }
    //    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    //    {
    //        Util.WriteLine("MyContext2.OnConfiguring called");
    //        builder.UseSqlServer(Util.connection_string);
    //    }
    //}
} // namespace
