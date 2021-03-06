﻿using ScalabelSimpleBlog.Data.Entities;
using ScalabelSimpleBlog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.Data.Repositories
{
    public class ApplicationContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected const int UsersCount = 1000;

        protected const int ArticlesCount = 1000;

        protected const int MinCommentsPerArticle = 5;
        protected const int MaxCommantsPerArticle = 60;

        protected const int MinClicksPerArticle = 20;
        protected const int MaxClicksPerArticle = 400;

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            if (context.Users.Count() == 0)
            {
                Console.WriteLine("Start adding users");
                // Paswword asdasd
                for (int i = 0; i < UsersCount; i++)
                {
                    var name = SeedExtensions.Names.Random();
                    context.Users.Add(new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = string.Format("test{0}@test.com",i),
                        PasswordHash = "AKmlHZ4oB9EeQUZidcqf2AXBCXhWFOu055EOjzbs4aUIlvYNW03pECNKFFkDyD+9Sg==",
                        SecurityStamp = Guid.NewGuid().ToString(),
                        LockoutEnabled = true,
                        UserName = string.Format("test{0}@test.com", i),
                        FirstName = name.Split(' ').First(),
                        LastName = name.Split(' ').Last()
                    });
                }

                Console.WriteLine("End adding users");
            }
            if(context.Tags.Count() == 0)
            {
                Console.WriteLine("Start adding Tags");
                context.Tags.Add(new Tag { Name = "IT" });
                context.Tags.Add(new Tag { Name = "Bussines" });
                context.Tags.Add(new Tag { Name = "Culture" });
                context.Tags.Add(new Tag { Name = "Software" });
                context.Tags.Add(new Tag { Name = "Sport" });
                context.Tags.Add(new Tag { Name = "Politics" });
                context.Tags.Add(new Tag { Name = "Life" });
                context.Tags.Add(new Tag { Name = "Fun" });
                context.SaveChanges();

                Console.WriteLine("End adding Tags");


                Console.WriteLine("Start adding Article");
                // GenerateArticles(context);
                Console.WriteLine("End adding Article");

                //Console.WriteLine("Start adding Comments");
                //GenerateCommentsForArticles(context);
                //Console.WriteLine("End adding Comments");

                //Console.WriteLine("Start adding Clicks");
                //GenerateStatisticForArticles(context);
                //Console.WriteLine("End adding Clicks");
            }
        }

        public void GenerateCommentsForArticles(ApplicationDbContext context)
        {
            var random = new Random();
            var users = context.Users.ToList();
            var articles = context.Articles.ToList();

            foreach (var article in articles)
            {
                var count = random.Next(MinCommentsPerArticle, MaxCommantsPerArticle);
                for (int i = 0; i < count; i++)
                {
                    context.Comments.Add(new Comment
                    {
                        ArticleId = article.Id,
                        CreatedDate =
                            DateTime.UtcNow.AddDays(0 - random.Next(5, 100))
                                .AddHours(0 - random.Next(0, 24))
                                .AddMinutes(0 - random.Next(0, 60)),
                        AuthorId = users.Random().Id,
                        Body = SeedExtensions.Headers.Random() + " " + SeedExtensions.Headers.Random()
                    });
                }
            }
        }

        public static void GenerateStatisticForArticles(ApplicationDbContext context)
        {
            var random = new Random();
            var users = context.Users.ToList();
            var articles = context.Articles.ToList();
            foreach (var article in articles)
            {
                var viewCount = random.Next(MinClicksPerArticle, MaxClicksPerArticle);
                for (int i = 0; i < viewCount; i++)
                {
                    context.StatiscticArticleViews.Add(new StatiscticArticleView
                    {
                        ArticleId = article.Id,
                        Time = DateTime.UtcNow.AddDays(0 - random.Next(5, 100)).AddHours(0 - random.Next(0, 24)).AddMinutes(0 - random.Next(0, 60)),
                        UserId = random.Next(0, 100) > 15 ? users.Random().Id : null
                    });

                    
                }
                context.SaveChanges();
            }
        }

        private static void GenerateArticles(ApplicationDbContext context)
        {
            var random = new Random();
            var tagsList = context.Tags.ToList();
            var users = context.Users.ToList();
            int i = 0;
            while (i < ArticlesCount)
            {
                var article = new Article();
                article.AddTagIfNotAdded(tagsList.Random());
                article.AddTagIfNotAdded(tagsList.Random());
                article.Header = SeedExtensions.Headers.Random();
                article.TeaserText = SeedExtensions.TeaserTexts.Random();
                article.Body = SeedExtensions.Body;
                article.CreatedDate = DateTime.UtcNow.AddDays(0 - random.Next(5, 100));
                article.IsPublished = random.Next(0, 100) > 15;
                article.AuthorId = users.Random().Id;
                context.Articles.Add(article);
                i++;
            }
            context.SaveChanges();
        }
    }

    public static class SeedExtensions
    {
        public static List<string> Headers = new List<string>
        {
            "Islamic State is Minting Gold Coins",
            "Putin and Medvedev Tuck Into Barbecue as Russia Enters Recession",
            "Photographer: Jason Alden/Bloomberg The Freshest Thing About the World’s Biggest Online Grocer Isn’t Food",
            "Sanders Within Striking Distance of Clinton in Iowa",
            "The Only Thing Serena Williams Struggles to Win Is Endorsements",
            "Uber Hires Engineers Who Remotely Hacked Into Jeep",
            "Meet the Maker: Savile Row's Woman Wunderkind",
            "Fed, ECB, BOE Officials All Say They See Inflation Rising",
            "Suzuki Plans to Go It Alone After Ending Four-Year VW Dispute",
            "Brilliantly Marketed' Banks in Sweden Can't Kill CoCo Doubts",
            "Ageas Gets $1.38 Billion From Hong Kong Unit Sale to JD Capital",
            "South Africa Sees Rhino Poaching Intensify as 749 Animals Killed",
            "Scott Walker: Building a Wall Along U.S. Border with Canada a 'Legitimate Issue'",
            "On Sunday Shows, Bernie Sanders Reflects on Iowa Poll Momentum",
            "Presidential Candidates on Both Sides Confronting Angry Iowans",
            "His exquisite sincerity education shameless ten earnestly breakfast add.",
            "So we me unknown as improve hastily sitting forming. Especially favourable compliment but thoroughly unreserved saw she themselves.",
            "Sufficient impossible him may ten insensible put continuing. ",
            "Oppose exeter income simple few joy cousin but twenty. ",
            "Scale began quiet up short wrong in in.",
            "Sportsmen shy forfeited engrossed may can. ",
            "Why end might ask civil again spoil. ",
            "She dinner she our horses depend. ",
            "Remember at children by reserved to vicinity.",
            "In affronting unreserved delightful simplicity ye.",
            "Law own advantage furniture continual sweetness bed agreeable perpetual. ",
            "Oh song well four only head busy it. ",
            "Afford son she had lively living.",
            "Tastes lovers myself too formal season our valley boy. ",
            "Lived it their their walls might to by young. ",
            "Building mr concerns servants in he outlived am breeding. ",
            "He so lain good miss when sell some at if.",
            "Told hand so an rich gave next.",
            "How doubt yet again see son smart. ",
            "While mirth large of on front. ",
            "Ye he greater related adapted proceed entered an. ",
            "Through it examine express promise no.",
            "Past add size game cold girl off how old. ",
            "Fat son how smiling mrs natural expense anxious friends.",
            "Boy scale enjoy ask abode fanny being son. ",
            "As material in learning subjects so improved feelings. ",
            "Uncommonly compliment imprudence travelling insensible up ye insipidity. "
        };

        public static List<string> Names = new List<string>
        {
            "Evelyn Cook",
            "Lois Thompson",
            "Annie Miller",
            "Doris Ramirez",
            "Randy Campbell",
            "Billy Coleman",
            "Juan Cooper"
        };

        public static List<string> TeaserTexts = new List<string>
        {
            "Her companions instrument set estimating sex remarkably solicitude motionless. Property men the why smallest graceful day insisted required. Inquiry justice country old placing sitting any ten age. Looking venture justice in evident in totally he do ability. Be is lose girl long of up give. Trifling wondered unpacked ye at he. In household certainty an on tolerably smallness difficult. Many no each like up be is next neat. Put not enjoyment behaviour her supposing. At he pulled object others.",
            "Is post each that just leaf no. He connection interested so we an sympathize advantages. To said is it shed want do. Occasional middletons everything so to. Have spot part for his quit may. Enable it is square my an regard. Often merit stuff first oh up hills as he. Servants contempt as although addition dashwood is procured. Interest in yourself an do of numerous feelings cheerful confined.",
            "With my them if up many. Lain week nay she them her she. Extremity so attending objection as engrossed gentleman something. Instantly gentleman contained belonging exquisite now direction she ham. West room at sent if year. Numerous indulged distance old law you. Total state as merit court green decay he. Steepest sex bachelor the may delicate its yourself. As he instantly on discovery concluded to. Open draw far pure miss felt say yet few sigh.",
            "At ourselves direction believing do he departure. Celebrated her had sentiments understood are projection set. Possession ye no mr unaffected remarkably at. Wrote house in never fruit up. Pasture imagine my garrets an he. However distant she request behaved see nothing. Talking settled at pleased an of me brother weather."
        };


        public static string Body = @"
<h2>Resources exquisite set arranging moonlight sex him household had</h2>

<p>Improved own provided blessing may peculiar domestic. Sight house has sex never. No visited raising gravity outward subject my cottage mr be. Hold do at tore in park feet near my case. Invitation at understood occasional sentiments insipidity inhabiting in. Off melancholy alteration principles old. Is do speedily kindness properly oh. Respect article painted cottage he is offices parlors.</p>

<p>Subjects to ecstatic children he. Could ye leave up as built match. Dejection agreeable attention set suspected led offending. Admitting an performed supposing by. Garden agreed matter are should formed temper had. Full held gay now roof whom such next was. Ham pretty our people moment put excuse narrow. Spite mirth money six above get going great own. Started now shortly had for assured hearing expense. Led juvenile his laughing speedily put pleasant relation offering.</p>

<p>He moonlight difficult engrossed an it sportsmen. Interested has all devonshire difficulty gay assistance joy. Unaffected at ye of compliment alteration to. Place voice no arise along to. Parlors waiting so against me no. Wishing calling are warrant settled was luckily. Express besides it present if at an opinion visitor.</p>

<p>Prepared do an dissuade be so whatever steepest. Yet her beyond looked either day wished nay. By doubtful disposed do juvenile an. Now curiosity you explained immediate why behaviour. An dispatched impossible of of melancholy favourable. Our quiet not heart along scale sense timed. Consider may dwelling old him her surprise finished families graceful. Gave led past poor met fine was new.</p>

<p>Her companions instrument set estimating sex remarkably solicitude motionless. Property men the why smallest graceful day insisted required. Inquiry justice country old placing sitting any ten age. Looking venture justice in evident in totally he do ability. Be is lose girl long of up give. Trifling wondered unpacked ye at he. In household certainty an on tolerably smallness difficult. Many no each like up be is next neat. Put not enjoyment behaviour her supposing. At he pulled object others.</p>
";

        public static void AddTagIfNotAdded(this Article article, Tag tag)
        {
            if (article.Tags.Any(x => x.Id == tag.Id))
                return;
            article.Tags.Add(tag);
            // tag.Articles.Add(article);
            Debug.Write("Added category" + tag.Name);
        }



    }

    public static class EnumerableHelper<E>
    {
        private static Random r;

        static EnumerableHelper()
        {
            r = new Random();
        }

        public static T Random<T>(IEnumerable<T> input)
        {
            return input.ElementAt(r.Next(input.Count()));
        }
    }

    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> input)
        {
            return EnumerableHelper<T>.Random(input);
        }
    }
}
