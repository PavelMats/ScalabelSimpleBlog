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

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            if(context.Tags.Count() == 0)
            {
                context.Tags.Add(new Tag { Name = "IT" });
                context.Tags.Add(new Tag { Name = "Bussines" });
                context.Tags.Add(new Tag { Name = "Culture" });
                context.Tags.Add(new Tag { Name = "Software" });
                context.Tags.Add(new Tag { Name = "Sport" });
                context.Tags.Add(new Tag { Name = "Politics" });
                context.SaveChanges();

                GenerateArticles(context);
            }
        }

        private static void GenerateArticles(ApplicationDbContext context)
        {
            var random = new Random();
            var tagsList = context.Tags.ToList();
            int i = 0;
            while(i < 100)
            {
                var article = new Article();
                article.AddTagIfNotAdded(tagsList.Random());
                article.AddTagIfNotAdded(tagsList.Random());
                article.Header = SeedExtensions.Headers.Random();
                article.TeaserText = SeedExtensions.TeaserTexts.Random();
                article.Body = SeedExtensions.Body;
                article.CreatedDate = DateTime.UtcNow.AddDays(0 - random.Next(5, 100));
                article.IsPublished = random.Next(0, 100) > 20;
                context.Articles.Add(article);
                i++;
            }
            context.SaveChanges();
        }
    }

    public static class SeedExtensions
    {
        internal static List<string> Headers = new List<string>
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
            "Presidential Candidates on Both Sides Confronting Angry Iowans"
        };

        internal static List<string> TeaserTexts = new List<string>
        {
            "Her companions instrument set estimating sex remarkably solicitude motionless. Property men the why smallest graceful day insisted required. Inquiry justice country old placing sitting any ten age. Looking venture justice in evident in totally he do ability. Be is lose girl long of up give. Trifling wondered unpacked ye at he. In household certainty an on tolerably smallness difficult. Many no each like up be is next neat. Put not enjoyment behaviour her supposing. At he pulled object others.",
            "Is post each that just leaf no. He connection interested so we an sympathize advantages. To said is it shed want do. Occasional middletons everything so to. Have spot part for his quit may. Enable it is square my an regard. Often merit stuff first oh up hills as he. Servants contempt as although addition dashwood is procured. Interest in yourself an do of numerous feelings cheerful confined.",
            "With my them if up many. Lain week nay she them her she. Extremity so attending objection as engrossed gentleman something. Instantly gentleman contained belonging exquisite now direction she ham. West room at sent if year. Numerous indulged distance old law you. Total state as merit court green decay he. Steepest sex bachelor the may delicate its yourself. As he instantly on discovery concluded to. Open draw far pure miss felt say yet few sigh.",
            "At ourselves direction believing do he departure. Celebrated her had sentiments understood are projection set. Possession ye no mr unaffected remarkably at. Wrote house in never fruit up. Pasture imagine my garrets an he. However distant she request behaved see nothing. Talking settled at pleased an of me brother weather."
        };


        internal static string Body = @"
<h2>Resources exquisite set arranging moonlight sex him household had</h2>

<p>Improved own provided blessing may peculiar domestic. Sight house has sex never. No visited raising gravity outward subject my cottage mr be. Hold do at tore in park feet near my case. Invitation at understood occasional sentiments insipidity inhabiting in. Off melancholy alteration principles old. Is do speedily kindness properly oh. Respect article painted cottage he is offices parlors.</p>

<p>Subjects to ecstatic children he. Could ye leave up as built match. Dejection agreeable attention set suspected led offending. Admitting an performed supposing by. Garden agreed matter are should formed temper had. Full held gay now roof whom such next was. Ham pretty our people moment put excuse narrow. Spite mirth money six above get going great own. Started now shortly had for assured hearing expense. Led juvenile his laughing speedily put pleasant relation offering.</p>

<p>He moonlight difficult engrossed an it sportsmen. Interested has all devonshire difficulty gay assistance joy. Unaffected at ye of compliment alteration to. Place voice no arise along to. Parlors waiting so against me no. Wishing calling are warrant settled was luckily. Express besides it present if at an opinion visitor.</p>

<p>Prepared do an dissuade be so whatever steepest. Yet her beyond looked either day wished nay. By doubtful disposed do juvenile an. Now curiosity you explained immediate why behaviour. An dispatched impossible of of melancholy favourable. Our quiet not heart along scale sense timed. Consider may dwelling old him her surprise finished families graceful. Gave led past poor met fine was new.</p>

<p>Her companions instrument set estimating sex remarkably solicitude motionless. Property men the why smallest graceful day insisted required. Inquiry justice country old placing sitting any ten age. Looking venture justice in evident in totally he do ability. Be is lose girl long of up give. Trifling wondered unpacked ye at he. In household certainty an on tolerably smallness difficult. Many no each like up be is next neat. Put not enjoyment behaviour her supposing. At he pulled object others.</p>
";

        internal static void AddTagIfNotAdded(this Article article, Tag tag)
        {
            if (article.Tags.Any(x => x.Id == tag.Id))
                return;
            article.Tags.Add(tag);
            tag.Articles.Add(article);
            Debug.Write("Added category" + tag.Name);
        }

    }

    internal static class EnumerableHelper<E>
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

    internal static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> input)
        {
            return EnumerableHelper<T>.Random(input);
        }
    }
}
