
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Jörmungandr
{
    static class Art
    {
        // Art for Jellyfish enemy for the JellyEnemy class
        public static Texture2D Boat { get; private set; }
        public static Texture2D Rock { get; private set; }
        public static Texture2D Jellyfish { get; private set; }
        public static Texture2D Mine { get; private set; }
        public static Texture2D Hide { get; private set; }
        public static Texture2D OceanBackground { get; private set; }
        public static Texture2D FinalBoss { get; private set; }
        public static Texture2D MidBoss { get; private set; }
        public static Texture2D JellyBullet { get; private set; }
        public static Texture2D RedMine { get; private set; }
        public static Texture2D MineBullet { get; private set; }
        public static Texture2D FullHeart { get; private set; }
        public static Texture2D EmptyHeart { get; private set; }
        public static Texture2D TopUIBar { get; private set; }
        public static Texture2D UIWinBox { get; private set; }
        public static Texture2D UILoseBox { get; private set; }
        public static Texture2D FinalBossBullet { get; private set; }
        public static Texture2D HealthCapsule { get; private set; }
        public static Texture2D MenuBackground { get; private set; }
        public static Texture2D SettingsBackground { get; private set; }
        public static Texture2D PauseBackground { get; private set; }
        public static Texture2D MenuButton { get; private set; }
        public static Texture2D EditMenuButton { get; private set; }
        public static Texture2D FloatingCannonLeft { get; private set; }
        public static Texture2D FloatingCannonRight { get; private set; }
        public static Texture2D CaveBackground { get; private set; }
        public static Texture2D CaveToOceanBackground { get; private set; }
        public static Texture2D CaveToOceanBackgroundBig { get; private set; }
        public static Texture2D OceanBackgroundResized { get; private set; }
        public static Texture2D OceanToSkyBackground { get; private set; }
        public static Texture2D OceanToSkyBackgroundBig { get; private set; }
        public static Texture2D SkyBackground { get; private set; }
        public static Texture2D SmallMenuButton { get; private set; }

        public static void Load(ContentManager content)
        {
            // Load content for .png location
            Boat = content.Load<Texture2D>("boat");
            Rock = content.Load<Texture2D>("Rock32x32v2");
            Jellyfish = content.Load<Texture2D>("jelly");
            Mine = content.Load<Texture2D>("mine64x64black");
            Hide = content.Load<Texture2D>("hide");
            OceanBackground = content.Load<Texture2D>("ocean");
            FinalBoss = content.Load<Texture2D>("finalBoss");
            MidBoss = content.Load<Texture2D>("midboss");
            JellyBullet = content.Load<Texture2D>("bulletPurp");
            RedMine = content.Load<Texture2D>("mine64x64red");
            MineBullet = content.Load<Texture2D>("mineBullet");
            FullHeart = content.Load<Texture2D>("FullHeart62x62");
            EmptyHeart = content.Load<Texture2D>("EmptyHeart62x62");
            TopUIBar = content.Load<Texture2D>("TopUIBar");
            UIWinBox = content.Load<Texture2D>("BoxGreen");
            UILoseBox = content.Load<Texture2D>("BoxRed");
            FinalBossBullet = content.Load<Texture2D>("finalBossBullet");
            HealthCapsule = content.Load<Texture2D>("healthcapsule");
            MenuBackground = content.Load<Texture2D>("MenuBackground");
            PauseBackground = content.Load<Texture2D>("PauseMenuBackground");
            SettingsBackground = content.Load<Texture2D>("SettingsBackground");
            MenuButton = content.Load<Texture2D>("Button200x60");
            EditMenuButton = content.Load<Texture2D>("EditButton");
            SmallMenuButton = content.Load<Texture2D>("SmallButton");

            FloatingCannonLeft = content.Load<Texture2D>("FloatingCannonLeft");
            FloatingCannonRight = content.Load<Texture2D>("FloatingCannonRight");
            CaveBackground = content.Load<Texture2D>("cave");
            CaveToOceanBackground = content.Load<Texture2D>("caveToOcean");
            OceanBackground = content.Load<Texture2D>("ocean");
            OceanToSkyBackground = content.Load<Texture2D>("oceanToSky");
            SkyBackground = content.Load<Texture2D>("sky");
        }
    }
}
