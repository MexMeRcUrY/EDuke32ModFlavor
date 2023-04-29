/*
Copyright (C) 2010-2019 EDuke32 developers and contributors
Copyright (C) 2022 MexMercury MBlood documentation tool
This file is part of MBlood.
MBlood and its tools is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License version 2
as published by the Free Software Foundation.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MBlood_ModDocumentation.DefParser;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Drawing;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
using System.Numerics;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;


namespace MBlood_ModDocumentation
{
    public class DefParser
    {
        //CSV Variabkles
        DataTable tblCSVOutput = new DataTable();
        DataRow curRow;
        List<DataColumn> OutputColumns = new List<DataColumn>();
        public static string SYSCHAR = "COMMON_";
        private List<DataColumn> commonCSVColumns = new List<DataColumn> {
                        new DataColumn(SYSCHAR+"MODNAME",typeof(string)),
                        new DataColumn(SYSCHAR+"TYPE",typeof(string)),
                        new DataColumn(SYSCHAR+"CATEGORY",typeof(string)),
                        new DataColumn(SYSCHAR+"DESCRIPTION",typeof(string)),
                        new DataColumn(SYSCHAR+"FILENAME",typeof(string)),
                        new DataColumn(SYSCHAR+"FILEEXTENSION",typeof(string)),
                        new DataColumn(SYSCHAR+"IMAGEMODE",typeof(string)),
                        new DataColumn(SYSCHAR+"PALLETNUMBER",typeof(int)),
                        new DataColumn(SYSCHAR+"TILENUMBER",typeof(int)),
                        };

        public static List<string> mandtoryCSVFields = new List<string> {
                        SYSCHAR+"MODNAME",
                        SYSCHAR+"TYPE",
                        SYSCHAR+"TILENUMBER",
                        SYSCHAR+"CATEGORY",
                        SYSCHAR+"DESCRIPTION",
                        SYSCHAR+"FILENAME",
                        SYSCHAR+"FILEEXTENSION",
                        SYSCHAR+"IMAGEMODE",
                        SYSCHAR+"PALLETNUMBER",
                        SYSCHAR+"SCALE",
                        SYSCHAR+"SHADE",
                        SYSCHAR+"FRAMENAME",
                        SYSCHAR+"SKINFILENAME",
                        SYSCHAR + "SKINFILEEXTENSION"}.Select(x => x.ToUpper()).ToList();

        private List<DataColumn> mandtoryCSVColumns = new List<DataColumn> {
                        new DataColumn(SYSCHAR+"MODNAME",typeof(string)),
                        new DataColumn(SYSCHAR+"TYPE",typeof(string)),
                        new DataColumn(SYSCHAR+"TILENUMBER",typeof(int)),
                        new DataColumn(SYSCHAR+"CATEGORY",typeof(string)),
                        new DataColumn(SYSCHAR+"DESCRIPTION",typeof(string)),
                        new DataColumn(SYSCHAR+"FILENAME",typeof(string)),
                        new DataColumn(SYSCHAR+"FILEEXTENSION",typeof(string)),
                        new DataColumn(SYSCHAR+"IMAGEMODE",typeof(string)),
                        new DataColumn(SYSCHAR+"PALLETNUMBER",typeof(int)),
                        new DataColumn(SYSCHAR+"SCALE",typeof(float)),
                        new DataColumn(SYSCHAR+"SHADE",typeof(float)),
                        new DataColumn(SYSCHAR+"FRAMENAME",typeof(string)),
                        new DataColumn(SYSCHAR+"SKINFILENAME",typeof(string)),
                        new DataColumn(SYSCHAR + "SKINFILEEXTENSION",typeof(string))};


        public DefParser()
        {
            //create mandatory fields
            foreach (DataColumn mandatoryColumn in mandtoryCSVColumns)
            {
                this.AddCSVColumn(mandatoryColumn);
            }
        }
        StringBuilder errorMsg = new StringBuilder();

        public int g_DEFLanguage_LoadSequence = 0;
        public int DEFLanguage_LoadSequence
        {
            get { return g_DEFLanguage_LoadSequence; }
            set { g_DEFLanguage_LoadSequence = value; }
        }
        public struct tokenlist
        {
            public string text;
            public scriptTokens tokenid;
            public Type type;
        }

        public enum scriptTokens
        {
            T_EOF = -2,
            T_ERROR = -1,
            T_INCLUDE = 0,
            T_DEFINE,
            T_DEFINETEXTURE,
            T_DEFINESKYBOX,
            T_DEFINETINT,
            T_DEFINEMODEL,
            T_DEFINEMODELFRAME,
            T_DEFINEMODELANIM,
            T_DEFINEMODELSKIN,
            T_SELECTMODELSKIN,
            T_DEFINEVOXEL,
            T_DEFINEVOXELTILES,
            T_MODEL,
            T_FILE,
            T_SCALE,
            T_SHADE,
            T_FRAME,
            T_SMOOTHDURATION,
            T_ANIM,
            T_SKIN,
            T_SURF,
            T_TILE,
            T_TILE0,
            T_TILE1,
            T_FRAME0,
            T_FRAME1,
            T_FPS,
            T_FLAGS,
            T_PAL,
            T_BASEPAL,
            T_DETAIL,
            T_GLOW,
            T_SPECULAR,
            T_NORMAL,
            T_PARAM,
            T_HUD,
            T_XADD,
            T_YADD,
            T_ZADD,
            T_ANGADD,
            T_FOV,
            T_FLIPPED,
            T_HIDE,
            T_NOBOB,
            T_NODEPTH,
            T_VOXEL,
            T_SKYBOX,
            T_FRONT, T_RIGHT, T_BACK, T_LEFT, T_TOP, T_BOTTOM,
            T_HIGHPALOOKUP,
            T_TINT,
            T_MAKEPALOOKUP, T_REMAPPAL, T_REMAPSELF,
            T_NOFLOORPAL, T_FLOORPAL,
            T_RED, T_GREEN, T_BLUE,
            T_TEXTURE, T_ALPHACUT, T_XSCALE, T_YSCALE, T_SPECPOWER, T_SPECFACTOR, T_NOCOMPRESS, T_NODOWNSIZE,
            T_FORCEFILTER,
            T_ARTQUALITY,
            T_INDEXED,
            T_ORIGSIZEX, T_ORIGSIZEY,
            T_UNDEFMODEL, T_UNDEFMODELRANGE, T_UNDEFMODELOF, T_UNDEFTEXTURE, T_UNDEFTEXTURERANGE,
            T_ALPHAHACK, T_ALPHAHACKRANGE,
            T_SPRITECOL, T_2DCOL, T_2DCOLIDXRANGE,
            T_FOGPAL,
            T_LOADGRP,
            T_DUMMYTILE, T_DUMMYTILERANGE,
            T_SETUPTILE, T_SETUPTILERANGE,
            T_UNDEFINETILE, T_UNDEFINETILERANGE,
            T_ANIMTILERANGE,
            T_CACHESIZE,
            T_IMPORTTILE,
            T_TILEFROMTEXTURE, T_XOFFSET, T_YOFFSET, T_TEXHITSCAN, T_NOFULLBRIGHT,
            T_ARTFILE,
            T_INCLUDEDEFAULT,
            T_NOFLOORPALRANGE,
            T_TEXHITSCANRANGE,
            T_NOFULLBRIGHTRANGE,
            T_MAPINFO, T_MAPFILE, T_MAPTITLE, T_MAPMD4, T_MHKFILE, T_MAPART,
            T_ECHO,
            T_GLOBALFLAGS,
            T_COPYTILE,
            T_MULTIPSKY, T_HORIZFRAC, T_LOGNUMTILES,
            T_BASEPALETTE, T_PALOOKUP, T_BLENDTABLE,
            T_RAW, T_OFFSET, T_SHIFTLEFT, T_NOSHADES, T_COPY,
            T_NUMALPHATABS,
            T_UNDEF,
            T_UNDEFBASEPALETTERANGE, T_UNDEFPALOOKUPRANGE, T_UNDEFBLENDTABLERANGE,
            T_GLBLEND, T_FORWARD, T_REVERSE, T_BOTH, T_SRC, T_DST, T_ALPHA,
            T_ZERO, T_ONE,
            T_SRC_COLOR, T_ONE_MINUS_SRC_COLOR,
            T_SRC_ALPHA, T_ONE_MINUS_SRC_ALPHA,
            T_DST_ALPHA, T_ONE_MINUS_DST_ALPHA,
            T_DST_COLOR, T_ONE_MINUS_DST_COLOR,
            T_SHADERED, T_SHADEGREEN, T_SHADEBLUE,
            T_SHADEFACTOR,
            T_IFCRC, T_IFMATCH, T_CRC32,
            T_SIZE,
            T_LOCALIZATION, T_STRING,
            T_TILEFONT, T_CHARACTER,
            T_TRUENPOT,
            T_STUB_INTEGER, T_STUB_INTEGER_STRING, T_STUB_BRACES, T_STUB_STRING_BRACES,
            T_NOTRANS,
        };

        List<tokenlist> basetokens = new List<tokenlist>()
        {
            new tokenlist() {text = "include", tokenid = scriptTokens.T_INCLUDE},
            new tokenlist() {text = "#include", tokenid = scriptTokens.T_INCLUDE},
            new tokenlist() {text = "includedefault", tokenid = scriptTokens.T_INCLUDEDEFAULT},
            new tokenlist() {text = "#includedefault", tokenid = scriptTokens.T_INCLUDEDEFAULT},
            new tokenlist() {text = "define", tokenid = scriptTokens.T_DEFINE},
            new tokenlist() {text = "#define", tokenid = scriptTokens.T_DEFINE},
            new tokenlist() {text = "definetexture", tokenid = scriptTokens.T_DEFINETEXTURE},
            new tokenlist() {text = "defineskybox", tokenid = scriptTokens.T_DEFINESKYBOX},
            new tokenlist() {text = "definetint", tokenid = scriptTokens.T_DEFINETINT},
            new tokenlist() {text = "definemodel", tokenid = scriptTokens.T_DEFINEMODEL},
            new tokenlist() {text = "definemodelframe", tokenid = scriptTokens.T_DEFINEMODELFRAME},
            new tokenlist() {text = "definemodelanim", tokenid = scriptTokens.T_DEFINEMODELANIM},
            new tokenlist() {text = "definemodelskin", tokenid = scriptTokens.T_DEFINEMODELSKIN},
            new tokenlist() {text = "selectmodelskin", tokenid = scriptTokens.T_SELECTMODELSKIN},
            new tokenlist() {text = "definevoxel", tokenid = scriptTokens.T_DEFINEVOXEL},
            new tokenlist() {text = "definevoxeltiles", tokenid = scriptTokens.T_DEFINEVOXELTILES},
            new tokenlist() {text = "model", tokenid = scriptTokens.T_MODEL},
            new tokenlist() {text = "voxel", tokenid = scriptTokens.T_VOXEL},
            new tokenlist() {text = "skybox", tokenid = scriptTokens.T_SKYBOX},
            new tokenlist() {text = "highpalookup", tokenid = scriptTokens.T_HIGHPALOOKUP},
            new tokenlist() {text = "tint", tokenid = scriptTokens.T_TINT},
            new tokenlist() {text = "makepalookup", tokenid = scriptTokens.T_MAKEPALOOKUP},
            new tokenlist() {text = "texture", tokenid = scriptTokens.T_TEXTURE},
            new tokenlist() {text = "tile", tokenid = scriptTokens.T_TEXTURE},
            new tokenlist() {text = "nofloorpalrange", tokenid = scriptTokens.T_NOFLOORPALRANGE},
            new tokenlist() {text = "texhitscanrange", tokenid = scriptTokens.T_TEXHITSCANRANGE},
            new tokenlist() {text = "nofullbrightrange", tokenid = scriptTokens.T_NOFULLBRIGHTRANGE},
            new tokenlist() {text = "undefmodel", tokenid = scriptTokens.T_UNDEFMODEL},
            new tokenlist() {text = "undefmodelrange", tokenid = scriptTokens.T_UNDEFMODELRANGE},
            new tokenlist() {text = "undefmodelof", tokenid = scriptTokens.T_UNDEFMODELOF},
            new tokenlist() {text = "undeftexture", tokenid = scriptTokens.T_UNDEFTEXTURE},
            new tokenlist() {text = "undeftexturerange", tokenid = scriptTokens.T_UNDEFTEXTURERANGE},
            new tokenlist() {text = "alphahack", tokenid = scriptTokens.T_ALPHAHACK},
            new tokenlist() {text = "alphahackrange", tokenid = scriptTokens.T_ALPHAHACKRANGE},
            new tokenlist() {text = "spritecol", tokenid = scriptTokens.T_SPRITECOL},
            new tokenlist() {text = "2dcol", tokenid = scriptTokens.T_2DCOL},
            new tokenlist() {text = "2dcolidxrange", tokenid = scriptTokens.T_2DCOLIDXRANGE},
            new tokenlist() {text = "fogpal", tokenid = scriptTokens.T_FOGPAL},
            new tokenlist() {text = "loadgrp", tokenid = scriptTokens.T_LOADGRP},
            new tokenlist() {text = "dummytile", tokenid = scriptTokens.T_DUMMYTILE},
            new tokenlist() {text = "dummytilerange", tokenid = scriptTokens.T_DUMMYTILERANGE},
            new tokenlist() {text = "setuptile", tokenid = scriptTokens.T_SETUPTILE},
            new tokenlist() {text = "setuptilerange", tokenid = scriptTokens.T_SETUPTILERANGE},
            new tokenlist() {text = "undefinetile", tokenid = scriptTokens.T_UNDEFINETILE},
            new tokenlist() {text = "undefinetilerange", tokenid = scriptTokens.T_UNDEFINETILERANGE},
            new tokenlist() {text = "animtilerange", tokenid = scriptTokens.T_ANIMTILERANGE},
            new tokenlist() {text = "cachesize", tokenid = scriptTokens.T_CACHESIZE},
            new tokenlist() {text = "dummytilefrompic", tokenid = scriptTokens.T_IMPORTTILE},
            new tokenlist() {text = "tilefromtexture", tokenid = scriptTokens.T_TILEFROMTEXTURE},
            new tokenlist() {text = "artfile", tokenid = scriptTokens.T_ARTFILE},
            new tokenlist() {text = "mapinfo", tokenid = scriptTokens.T_MAPINFO},
            new tokenlist() {text = "echo", tokenid = scriptTokens.T_ECHO},
            new tokenlist() {text = "globalflags", tokenid = scriptTokens.T_GLOBALFLAGS},
            new tokenlist() {text = "copytile", tokenid = scriptTokens.T_COPYTILE},
            new tokenlist() {text = "multipsky", tokenid = scriptTokens.T_MULTIPSKY},
            new tokenlist() {text = "basepalette", tokenid = scriptTokens.T_BASEPALETTE},
            new tokenlist() {text = "palookup", tokenid = scriptTokens.T_PALOOKUP},
            new tokenlist() {text = "blendtable", tokenid = scriptTokens.T_BLENDTABLE},
            new tokenlist() {text = "numalphatables", tokenid = scriptTokens.T_NUMALPHATABS},
            new tokenlist() {text = "undefbasepaletterange", tokenid = scriptTokens.T_UNDEFBASEPALETTERANGE},
            new tokenlist() {text = "undefpalookuprange", tokenid = scriptTokens.T_UNDEFPALOOKUPRANGE},
            new tokenlist() {text = "undefblendtablerange", tokenid = scriptTokens.T_UNDEFBLENDTABLERANGE},
            new tokenlist() {text = "shadefactor", tokenid = scriptTokens.T_SHADEFACTOR},
            new tokenlist() {text = "localization", tokenid = scriptTokens.T_LOCALIZATION},
            new tokenlist() {text = "tilefont", tokenid = scriptTokens.T_TILEFONT},
            new tokenlist() {text = "globalgameflags", tokenid = scriptTokens.T_STUB_INTEGER},
            new tokenlist() {text = "delplayercolor", tokenid = scriptTokens.T_STUB_INTEGER},
            new tokenlist() {text = "addplayercolor", tokenid = scriptTokens.T_STUB_INTEGER_STRING},
            new tokenlist() {text = "music", tokenid = scriptTokens.T_STUB_BRACES},
            new tokenlist() {text = "sound", tokenid = scriptTokens.T_STUB_BRACES},
            new tokenlist() {text = "newgamechoices", tokenid = scriptTokens.T_STUB_BRACES},
            new tokenlist() {text = "animsounds", tokenid = scriptTokens.T_STUB_STRING_BRACES},
            new tokenlist() {text = "cutscene", tokenid = scriptTokens.T_STUB_STRING_BRACES},
            new tokenlist() {text = "keyconfig", tokenid = scriptTokens.T_STUB_BRACES}
        };
        tokenlist[] modeltokens =
        new tokenlist[]{
        new tokenlist(){text = "scale", tokenid = scriptTokens.T_SCALE, type = typeof(float) },
                new tokenlist() { text = "shade", tokenid = scriptTokens.T_SHADE , type = typeof(int)},
                new tokenlist() { text =  "zadd", tokenid = scriptTokens.T_ZADD   , type = typeof(float)  },
                new tokenlist() { text = "yoffset", tokenid = scriptTokens.T_YOFFSET, type = typeof(float)  },
                new tokenlist() { text = "frame", tokenid = scriptTokens.T_FRAME    },
                new tokenlist() { text = "anim", tokenid = scriptTokens.T_ANIM     },
                new tokenlist() { text = "skin", tokenid = scriptTokens.T_SKIN     },
                new tokenlist() { text = "detail", tokenid = scriptTokens.T_DETAIL   },
                new tokenlist() { text = "glow", tokenid = scriptTokens.T_GLOW     },
                new tokenlist() { text = "specular", tokenid = scriptTokens.T_SPECULAR },
                new tokenlist() { text = "normal", tokenid = scriptTokens.T_NORMAL   },
                new tokenlist() { text = "hud", tokenid = scriptTokens.T_HUD      },
                new tokenlist() { text = "flags", tokenid = scriptTokens.T_FLAGS  , type = typeof(int)  },
            };
        tokenlist[] modelframetokens =
        new tokenlist[]{
          new tokenlist(){ text = "pal",tokenid = scriptTokens.T_PAL , type = typeof(int)},
          new tokenlist(){ text = "frame",tokenid = scriptTokens.T_FRAME , type = typeof(string)},
          new tokenlist(){ text = "name", tokenid = scriptTokens.T_FRAME , type = typeof(string)},
          new tokenlist(){ text = "tile",tokenid = scriptTokens.T_TILE, type = typeof(int)},
          new tokenlist(){ text = "tile0",tokenid = scriptTokens.T_TILE0 , type = typeof(int)},
          new tokenlist(){ text = "tile1",tokenid = scriptTokens.T_TILE1 , type = typeof(int)},
          new tokenlist(){ text = "smoothduration",tokenid = scriptTokens.T_SMOOTHDURATION, type = typeof(float)},
           };
        tokenlist[] modelanimtokens =
        new tokenlist[]{new tokenlist(){ text = "frame0", tokenid = scriptTokens.T_FRAME0 , type = typeof(int)},
                        new tokenlist(){ text = "frame1", tokenid = scriptTokens.T_FRAME1 , type = typeof(int)},
                        new tokenlist(){ text = "fps",    tokenid = scriptTokens.T_FPS    , type = typeof(int)},
                        new tokenlist(){ text = "flags",  tokenid = scriptTokens.T_FLAGS  , type = typeof(int)},
                    };
        //models tokens for skin , detail , glow , specular , normal  
        tokenlist[] modelCommonSubTokens =
        new tokenlist[]{
                        new tokenlist(){ text = "pal",tokenid = scriptTokens.T_PAL   , type = typeof(int)     },
                        new tokenlist(){ text = "file",tokenid = scriptTokens.T_FILE   , type = typeof(string)    },
                        new tokenlist(){ text = "surf",tokenid = scriptTokens.T_SURF  , type = typeof(int)     },
                        new tokenlist(){ text = "surface",tokenid = scriptTokens.T_SURF   , type = typeof(int)    },
                        new tokenlist(){ text = "intensity",tokenid = scriptTokens.T_PARAM   , type = typeof(float)   },
                        new tokenlist(){ text = "scale",tokenid = scriptTokens.T_PARAM   , type = typeof(float)   },
                        new tokenlist(){ text = "detailscale",tokenid = scriptTokens.T_PARAM   , type = typeof(float)   },
                        new tokenlist(){ text = "specpower",tokenid = scriptTokens.T_SPECPOWER  }, new tokenlist(){ text = "specularpower",tokenid = scriptTokens.T_SPECPOWER  }, new tokenlist(){ text = "parallaxscale", tokenid = scriptTokens.T_SPECPOWER },
                        new tokenlist(){ text = "specfactor",tokenid = scriptTokens.T_SPECFACTOR }, new tokenlist(){ text = "specularfactor", tokenid = scriptTokens.T_SPECFACTOR }, new tokenlist(){ text = "parallaxbias", tokenid = scriptTokens.T_SPECFACTOR },
                        new tokenlist(){ text = "nocompress",tokenid = scriptTokens.T_NOCOMPRESS },
                        new tokenlist(){ text = "nodownsize",tokenid = scriptTokens.T_NODOWNSIZE },
                        new tokenlist(){ text = "forcefilter",tokenid = scriptTokens.T_FORCEFILTER },
                        new tokenlist(){ text = "artquality",tokenid = scriptTokens.T_ARTQUALITY },
                    };
        tokenlist[] modelhudtokens =
    new tokenlist[]{
                        new tokenlist(){ text =  "tile", tokenid = scriptTokens.T_TILE  , type = typeof(int) },
                        new tokenlist(){ text =  "tile0",  tokenid = scriptTokens.T_TILE0 , type = typeof(int) },
                        new tokenlist(){ text =  "tile1",  tokenid = scriptTokens.T_TILE1  , type = typeof(int)},
                        new tokenlist(){ text =  "xadd",   tokenid = scriptTokens.T_XADD   , type = typeof(float)},
                        new tokenlist(){ text =  "yadd",   tokenid = scriptTokens.T_YADD   , type = typeof(float)},
                        new tokenlist(){ text =  "zadd",   tokenid = scriptTokens.T_ZADD  , type = typeof(float) },
                        new tokenlist(){ text =  "angadd", tokenid = scriptTokens.T_ANGADD , type = typeof(int)},
                        new tokenlist(){ text =  "fov",    tokenid = scriptTokens.T_FOV    },
                        new tokenlist(){ text =  "hide",   tokenid = scriptTokens.T_HIDE   },
                        new tokenlist(){ text =  "nobob",  tokenid = scriptTokens.T_NOBOB  },
                        new tokenlist(){ text =  "flipped",tokenid = scriptTokens.T_FLIPPED},
                        new tokenlist(){ text =  "nodepth",tokenid = scriptTokens.T_NODEPTH},
                    };
        tokenlist[] texturetokens =
            new tokenlist[]{
                new tokenlist(){ text =  "pal",     tokenid = scriptTokens.T_PAL  , type = typeof(int)},
                new tokenlist(){ text =  "detail",  tokenid = scriptTokens.T_DETAIL },
                new tokenlist(){ text =  "glow",    tokenid = scriptTokens.T_GLOW },
                new tokenlist(){ text =  "specular",tokenid = scriptTokens.T_SPECULAR },
                new tokenlist(){ text =  "normal",  tokenid = scriptTokens.T_NORMAL },
            };
        tokenlist[] texturetokens_pal =
            new tokenlist[]{
                        new tokenlist(){ text =  "file",tokenid = scriptTokens.T_FILE , type = typeof(string)},new tokenlist(){text ="name", tokenid = scriptTokens.T_FILE, type = typeof(string) },
                        new tokenlist(){ text =  "indexed",   tokenid =scriptTokens.T_INDEXED    },
                        new tokenlist(){ text =  "alphacut",tokenid =scriptTokens.T_ALPHACUT },
                        new tokenlist(){ text ="scale",  tokenid = scriptTokens.T_XSCALE , type = typeof(float)},new tokenlist(){ text =  "detailscale",tokenid = scriptTokens.T_XSCALE , type = typeof(float)}, new tokenlist(){  text ="xscale",  tokenid = scriptTokens.T_XSCALE , type = typeof(float)}, new tokenlist(){  text ="intensity",  tokenid = scriptTokens.T_XSCALE , type = typeof(float)},
                        new tokenlist(){ text =  "yscale",tokenid =scriptTokens.T_YSCALE , type = typeof(float)},
                        new tokenlist(){ text =  "specpower",tokenid =scriptTokens.T_SPECPOWER }, new tokenlist(){ text = "specularpower",  tokenid =scriptTokens.T_SPECPOWER }, new tokenlist(){  text ="parallaxscale",  tokenid =scriptTokens.T_SPECPOWER },
                        new tokenlist(){ text =  "specfactor",tokenid =scriptTokens.T_SPECFACTOR }, new tokenlist(){ text = "specularfactor",  tokenid =scriptTokens.T_SPECFACTOR }, new tokenlist(){  text ="parallaxbias",  tokenid =scriptTokens.T_SPECFACTOR },
                        new tokenlist(){ text =  "nocompress",tokenid =scriptTokens.T_NOCOMPRESS ,type = typeof(bool)},
                        new tokenlist(){ text =  "nodownsize",tokenid =scriptTokens.T_NODOWNSIZE ,type = typeof(bool)},
                        new tokenlist(){ text =  "forcefilter",tokenid =scriptTokens.T_FORCEFILTER ,type = typeof(bool)},
                        new tokenlist(){ text =  "artquality",tokenid =scriptTokens.T_ARTQUALITY },

                        new tokenlist(){ text =  "orig_sizex",tokenid =scriptTokens.T_ORIGSIZEX }, new tokenlist(){ text ="orig_sizey",  tokenid =scriptTokens.T_ORIGSIZEY }
        };
        //public tokenlist getatoken(string script, tokenlist[] tl)
        //{
        //    return new tokenlist();
        //}
        public void ReadDEF(string path, out StringBuilder errors)
        {
            errors = errorMsg;
            DEFLanguage_LoadSequence = 0; //reset script text line sequence output
            try
            {
                tokenlist tokn;
                bool newModelRow = false;
                string scriptFileText = File.ReadAllText(path);
                List<string> scriptTextList = scriptFileText.Split(new string[] { "}", "{", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                scriptTextList.RemoveAll(x => x.StartsWith("\r") || x.StartsWith("\n") || x.Trim() == String.Empty);
                string[] scriptText = scriptTextList.ToArray();
                //create
                string modelFileName = String.Empty;
                string modelFileNameExt = string.Empty;
                DataRow curModelRow = null;
                for (int s = 0; s < scriptText.Count(); s++)
                {
                    scriptText[s] = scriptText[s].Trim();
                    //skip comments or line feeds
                    if (scriptText[s].StartsWith(@"//") || scriptText[s].StartsWith("\r") || scriptText[s].StartsWith("\n"))
                    {
                        continue;
                    }

                    //Process all Base tokens
                    //Model Token
                    string scriptModelTkn = basetokens.Find(x => x.tokenid == scriptTokens.T_MODEL).text;
                    newModelRow = false;
                    if (scriptText[s].IndexOf(scriptModelTkn, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        var result = (from Match match in Regex.Matches(scriptText[s], "\"([^\"]*)\"")
                                      select match.ToString()).FirstOrDefault();

                        if (string.IsNullOrWhiteSpace(result))
                        {
                            errorMsg.AppendLine("Invalid MODEL file name");
                            continue;
                        }
                        result = result.Replace("\"", String.Empty);
                        modelFileName = Path.GetFileNameWithoutExtension(result);
                        modelFileNameExt = Path.GetExtension(result).Replace(".", string.Empty);
                        newModelRow = true;
                        WriteCSVRow("FILENAME", modelFileName, newModelRow); //new model row
                        WriteCSVRow("FILENAMEEXTENSION", modelFileNameExt);
                        WriteCSVRow("TYPE", "Model"); //default
                        //WriteCSVRow("TILENUMBER", "-1"); // -1 is just a default that will get replaced once the tile number is read from the script text file because the tile number comes later in the script file in the frame token
                        
                        curModelRow = curRow;
                        continue;
                    }
                    //Model Sub Tokens
                    List<string> scriptText_ModelTkns = null;
                    //foreach (string scriptModelSubTkn in modeltokens.Select(x => x.text))
                    //{
                    // if (scriptText[s].IndexOf(scriptModelSubTkn, StringComparison.OrdinalIgnoreCase) > -1)
                    // {
                    try
                    {
                        if (scriptText_ModelTkns == null)
                        {
                            scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                            scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                        }
                        for (int i = 0; i < scriptText_ModelTkns.Count; i++)
                        {
                            scriptTokens foundToken = getToken(scriptText_ModelTkns[i], modeltokens);
                            int scriptText_ModelTkns_Index = i;
                            bool newPallet = false;

                            switch (foundToken)
                            {
                                //has subTokens
                                case scriptTokens.T_SKIN:
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    writeSubToken(modelCommonSubTokens,"MODEL_SKIN", scriptText_ModelTkns_Index, scriptText_ModelTkns, modelFileName, modelFileNameExt);
                                    break;
                                //has subtokens
                                case scriptTokens.T_DETAIL:
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    writeSubToken(modelCommonSubTokens, "MODEL_DETAIL", scriptText_ModelTkns_Index, scriptText_ModelTkns, modelFileName, modelFileNameExt);
                                    break;
                                //has subtokens
                                case scriptTokens.T_GLOW:
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    writeSubToken(modelCommonSubTokens, "MODEL_GLOW", scriptText_ModelTkns_Index, scriptText_ModelTkns, modelFileName, modelFileNameExt);
                                    break;
                                //has subtokens
                                case scriptTokens.T_SPECULAR:
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    writeSubToken(modelCommonSubTokens, "MODEL_SPECULAR", scriptText_ModelTkns_Index, scriptText_ModelTkns, modelFileName, modelFileNameExt);
                                    break;
                                //has subtokens
                                case scriptTokens.T_NORMAL:
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    writeSubToken(modelCommonSubTokens, "MODEL_NORMAL", scriptText_ModelTkns_Index, scriptText_ModelTkns, modelFileName, modelFileNameExt);
                                    break;
                                //has subtokens
                                case scriptTokens.T_ANIM:
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    writeSubToken(modelanimtokens, "MODEL_ANIM", scriptText_ModelTkns_Index, scriptText_ModelTkns, modelFileName, modelFileNameExt);
                                    break;
                                    //has subTokens
                                case scriptTokens.T_FRAME:
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    writeSubToken(modelframetokens, "MODEL_FRAME", scriptText_ModelTkns_Index, scriptText_ModelTkns, modelFileName, modelFileNameExt);
                                    /*
                                    cloneCSVRow(curModelRow);
                                    s++;
                                    scriptText_ModelTkns = scriptText[s].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    for (int m = i; m < scriptText_ModelTkns.Count; m++)
                                    {
                                        var foundsubToken = getToken(scriptText_ModelTkns[m], modelframetokens);
                                        switch (foundsubToken)
                                        {
                                            case scriptTokens.T_FRAME:
                                                string frameName = Path.GetFileName(scriptText_ModelTkns[m + 1]).Replace("\"", String.Empty); ;
                                                WriteCSVRow("MODEL_FRAME_NAME", frameName);
                                                break;
                                            case scriptTokens.T_TILE:
                                                //updateClonedCSVRows("TILENUMBER", "-1", "TILENUMBER", scriptText_ModelTkns[m + 1]);
                                                WriteCSVRow("TILENUMBER", scriptText_ModelTkns[m + 1]);
                                                break;
                                            case scriptTokens.T_ERROR:
                                                continue;
                                            default:
                                                string tokenFrameText = modeltokens.Where(x => x.tokenid.Equals(foundToken)).FirstOrDefault().text.ToUpper();
                                                WriteCSVRow("MODEL_FRAME_" + tokenFrameText, scriptText_ModelTkns[m + 1]);
                                                break;
                                        }
                                    }
                                    */

                                    break;
                                case scriptTokens.T_ERROR: //token not found, mostly because becuase its a token value.
                                    continue;

                                default:
                                    string tokenText = modeltokens.Where(x => x.tokenid.Equals(foundToken)).FirstOrDefault().text.ToUpper();
                                    int indx = 0;
                                    if ((i == 0 && scriptText_ModelTkns.Count > 1) || i > 0)
                                    {
                                        indx = i + 1;
                                    }
                                    WriteCSVRow("MODEL_" + tokenText, scriptText_ModelTkns[indx]);
                                    break;
                            }
                            // }

                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg.AppendLine(" Error Reading Model Token: " + scriptText_ModelTkns + " -> " + ex.Message);
                    }


                    //  }
                    //}

                }

                //tokn.tokenid = 0;
                //switch (tokn.tokenid)
                //{
                //    case scriptTokens.T_ERROR:
                //        //LOG_F(ERROR, "%s:%d: unknown error.", script->filename, scriptfile_getlinum(script, cmdtokptr));
                //        break;
                //    case scriptTokens.T_EOF:
                //        break;
                //    case scriptTokens.T_DEFINEMODEL:
                //        {
                //            string modelfn;
                //            double scale;
                //            int shadeoffs;

                //            //if (scriptfile_getstring(script, &modelfn)) break;
                //            //if (scriptfile_getdouble(script, &scale)) break;
                //            //if (scriptfile_getnumber(script, &shadeoffs)) break;
                //        }
                //        break;
                //}
            }
            catch (Exception ex)
            {
                errorMsg.AppendLine(ex.Message);
            }
        }
        private void writeSubToken(tokenlist[] tokenlist, string tokenName, int scriptText_ModelTkns_startIndex, List<string> scriptText_ModelTkns, string modelFileName, string modelFileNameExt)
        {
            for (int m = scriptText_ModelTkns_startIndex; m < scriptText_ModelTkns.Count; m++)
            {
                var foundsubToken = getToken(scriptText_ModelTkns[m], tokenlist);
                switch (foundsubToken)
                {
                    //begin MODEL sub Tokens
                    case scriptTokens.T_PAL:
                        //pal<palnum>
                        //Specifies which palette this texture maps to.
                        //WHEN tokenName it is FRAME
                        //Makes the model definition exclusive for the this pal.This allow to assign different models for different pal.
                        WriteCSVRow("PALLETNUMBER", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_SURF:
                        //surface<surfnum>(or surf)
                        //Specifies which MD3 surface this texture should be applied to.This has no significance for MD2 models.
                        break;
                    case scriptTokens.T_FILE:
                        //file<filename>
                        //Specifies the texture file to use.File may be any PNG, JPG, DDS, TGA, BMP, GIF or PCX file
                        //IMPORTANT: If your model exists in a subdirectory(ie.the model filename includes a path to the .md2/ 3) you will need to give the same path to filename if the texture is in the same directory.
                        string skinFileName = Path.GetFileNameWithoutExtension(scriptText_ModelTkns[m + 1]).Replace("\"", String.Empty);
                        string skinFileNameExt = Path.GetExtension(scriptText_ModelTkns[m + 1]).Replace("\"", String.Empty).Replace(".", string.Empty);
                        WriteCSVRow(tokenName + "_FILENAME", skinFileName);
                        WriteCSVRow(tokenName + "_FILENAME_EXTENSION", skinFileNameExt);
                        break;
                    case scriptTokens.T_SCALE:
                    case scriptTokens.T_PARAM:
                        //scale<value>
                        //< value > is a positive floating - point value that'll determine how much your detail map should repeat on your diffuse map (if you want your detail map to repeat five times, use a 1/5 scale : 0.2).
                        //scale<value>(or detailscale / intensity)
                        WriteCSVRow(tokenName + "_SCALE", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_SPECPOWER:
                        WriteCSVRow(tokenName + "_SPECPOWER", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_SPECFACTOR:
                        WriteCSVRow(tokenName + "_SPECFACTOR", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_NOCOMPRESS:
                        WriteCSVRow(tokenName + "_NOCOMPRESS", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_NODOWNSIZE:
                        WriteCSVRow(tokenName + "_NODOWNSIZE", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_FORCEFILTER:
                        WriteCSVRow(tokenName + "_FORCEFILTER", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_ARTQUALITY:
                        WriteCSVRow(tokenName + "_ARTQUALITY", scriptText_ModelTkns[m + 1]);
                        break;
                    //begin ANIM Sub Toekns
                    case scriptTokens.T_FRAME0:
                        //Defines an animation from a group of frames in the model. The brace-enclosed block may contain these instructions:
                        //frame0 < start framename >
                        //Specifies the names of the start(frame0) and end(frame1) frames of the animation.
                        WriteCSVRow(tokenName + "_FRAME0", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_FRAME1:
                        //Defines an animation from a group of frames in the model. The brace-enclosed block may contain these instructions:
                        //frame1 < end framename >
                        //Specifies the names of the start(frame0) and end(frame1) frames of the animation.
                        WriteCSVRow(tokenName + "_FRAME1", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_FPS:
                        //fps<fps>
                        //Specifies the frame rate at which the animation should play.This value may be fractional.
                        WriteCSVRow(tokenName + "_FPS", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_FLAGS:
                        //flags<flags>
                        //Specifies any special properties the animation should have, the values of which should be added together to combine multiple options.
                        //Valid options are:
                        //0: none(looping animation)
                        //1: one - shot(plays beginning to end once and stops on the last frame).
                        WriteCSVRow(tokenName + "_FLAGS", scriptText_ModelTkns[m + 1]);
                        break;
                    //begin frame  sub tokens
                    case scriptTokens.T_FRAME:
                        //name<framename>(or frame)
                        //If<framename> is identical to the starting frame of an animation, the engine will play that animation, otherwise the replacement will be static. You can choose to use the frame or name versions of this instruction as both are identical.
                        WriteCSVRow(tokenName + "_NAME", scriptText_ModelTkns[m + 1]);
                        break;
                    //tile<tilenum>
                    //tile0 < start tilenum >
                    //tile1 < end tilenum >
                    //Use the tile instruction to specify an ART - file tile which this model should replace.Use the tile0 and tile1 instructions together to specify a range of ART - file tiles.If you use tile0, you must also have a tile1.You may not use the same instruction twice to specify multiple ranges.
                    case scriptTokens.T_TILE:
                        WriteCSVRow("TILENUMBER", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_TILE0:
                        WriteCSVRow(tokenName + "_TILE0", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_TILE1:
                        WriteCSVRow(tokenName + "_TILE1", scriptText_ModelTkns[m + 1]);
                        break;
                    case scriptTokens.T_SMOOTHDURATION:
                        //smoothduration<value>
                        //If smoothduration is non - zero switching from another animation to the one defined by that frame block will trigger an intermediary animation smoothing state of duration<value> seconds.
                        WriteCSVRow(tokenName + "_SMOOTHDURATION", scriptText_ModelTkns[m + 1]);
                        break;

                        /*
                         * TODO:
                         hud { [...] }

Defines a range of ART-file tiles to use with a heads-up-display. The brace-enclosed block may contain these instructions:

        tile <tilenum>
        tile0 <start tilenum>
        tile1 <end tilenum>

        tile0 and tile1 together specify a range of ART-file tiles which this model frame should replace when rendered as part of the HUD. You can specify individual tiles using a single tile command.

        xadd <offset>
        yadd <offset>
        zadd <offset>
        angadd <offset>

        Use these offsets to fine-tune the location of the model placement. xadd, yadd, and zadd are position offsets relative to the viewer's orienation. You can use floating point values with them. angadd is a Build angle offset. (512 90 degrees, 1024 180 degrees, etc...).

        fov

        DESCRIPTION

        hide

        Some weapons use multiple ART tiles for constructing the gun or animation. Use this option to hide parts that you don't need in your replacement.

        nobob

        By default, the HUD model offset is affected by the player bobbing offset when the player is walking. Use this option to disable that.

        flipped

        Use this option to apply the settings inside the hud block only if the object is normally rendered x-flipped (mirror image). Some weapons, such as the devastator, are rendered in 2 pieces, the left devastator is actually a mirror image of the right.

        nodepth

        Use this to render a HUD model without the use of the depth buffer. Normally, you should avoid this. The one exception where this is useful is for the spinning nuke menu icon because it should always be in front -- and it just happens to be convex, which is the one case that is safe with the depth buffer disabled.
                        */
                }
            }

        }
        public scriptTokens getToken(string scriptText, tokenlist[] scriptTokens)
        {
            scriptText = scriptText.Trim();
            var foundToken = scriptTokens.Where(x => scriptText.Equals(x.text, StringComparison.OrdinalIgnoreCase)).Select(x => x.tokenid);
            if (foundToken != null && foundToken.Any())
            {
                return foundToken.FirstOrDefault();
            }
            return DefParser.scriptTokens.T_ERROR; // NO TOKEN FOUND

        }
        //private T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        //{
        //    if (val.CompareTo(min) < 0) return min;
        //    else if (val.CompareTo(max) > 0) return max;
        //    else return val;
        //}
        public void WriteOutput(string path, string fileName)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = tblCSVOutput.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in tblCSVOutput.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            var outputPath = Path.Combine(path, fileName);
            File.WriteAllText(outputPath, sb.ToString());
        }
        private void WriteCSVRow(string columnName, string value, bool newRow = false)
        {

            float fValue = 0.0f;
            bool isNumeric = float.TryParse(value, out fValue);
            //posible to create new row to update.
            if (newRow)
            {
                AddCSVColumn("SEQUENCENUMBER", typeof(int));
            }
            //add column if does not exist
            if (isNumeric)
                AddCSVColumn(columnName, typeof(float));
            else
                AddCSVColumn(columnName, typeof(string));

            if (newRow)
            {
                curRow = tblCSVOutput.NewRow();
                curRow["SEQUENCENUMBER"] = ++DEFLanguage_LoadSequence;
                tblCSVOutput.Rows.Add(curRow);
            }
            if (isNumeric)
                curRow[columnName] = fValue;
            else
                curRow[columnName] = value;
        }
        private void cloneCSVRow(DataRow? dr = null)
        {
            DataRow desRow = tblCSVOutput.NewRow();
            
            if (curRow != null || dr != null)
            {
                if (dr != null && dr.ItemArray != null)
                {
                    desRow.ItemArray = dr.ItemArray.Clone() as object[];
                    desRow["SEQUENCENUMBER"] = ++DEFLanguage_LoadSequence;
                }
                else
                if (curRow != null && curRow.ItemArray != null)
                {
                    desRow.ItemArray = curRow.ItemArray.Clone() as object[];
                    desRow["SEQUENCENUMBER"] = ++DEFLanguage_LoadSequence;
                }
                tblCSVOutput.Rows.Add(desRow);
                curRow = desRow;
            }
        }
       
            private void updateClonedCSVRows(string column, string columnKey, string updColumnName, string updValue)
        {
            if (tblCSVOutput.Columns != null && tblCSVOutput.Columns.Count > 0)
            {
                DataColumn? col = tblCSVOutput.Columns[column];
                int intValue = 0;
                bool isNumeric = int.TryParse(columnKey, out intValue);
                string sValue = string.Empty;
                if (isNumeric)
                {
                    sValue = intValue.ToString();
                }
                else
                {
                    sValue = columnKey;
                }

                foreach (DataRow row in tblCSVOutput.Rows)
                {
                    if (row[col].ToString().Equals(sValue, StringComparison.OrdinalIgnoreCase))
                    {
                        isNumeric = int.TryParse(updValue, out intValue);
                        if (isNumeric)
                        {
                            row[updColumnName] = intValue;
                        }
                        else
                        {
                            row[updColumnName] = updValue;
                        }
                    }
                }

            }

        }
        private bool AddCSVColumn(DataColumn col)
        {
            if (!OutputColumns.Any(x => x.ColumnName.Equals(col.ColumnName, StringComparison.OrdinalIgnoreCase)))
            {
                OutputColumns.Add(col);
                return true;
            }
            return false;
        }
        private bool AddCSVColumn(string colName, Type colType)
        {
            var colExist = tblCSVOutput.Columns.Contains(colName);
            DataColumn col = new DataColumn(colName, colType);
            if (!colExist)
            {
                OutputColumns.Add(col);
                tblCSVOutput.Columns.Add(col);
                return true;
                //string? foundMandatoryCol = DefParser.mandtoryCSVFields.Find(x => x.IndexOf(SYSCHAR + colName, StringComparison.OrdinalIgnoreCase) > -1);
                //if (foundMandatoryCol != null)
                //{
                //    col = new DataColumn(foundMandatoryCol, colType);
                //    OutputColumns.Add(col);
                //}else
                //{
                //    OutputColumns.Add(col);
                //}
            }
            return false;
        }

        public void WriteTemplateFile(string path)
        {
            DataTable tblCSVOutput = new DataTable();
            foreach (var com in commonCSVColumns)
            {
                this.AddCSVColumn(com);
            }
            foreach (tokenlist textureToken in texturetokens_pal)
            {
                if (textureToken.tokenid == scriptTokens.T_FILE) //common column
                    continue;
                if (textureToken.tokenid == scriptTokens.T_XSCALE) //differnt namings xscale (aka scale, detailscale, intensity)
                {
                    this.AddCSVColumn("TEXTURE_SCALE", textureToken.type);
                    continue;
                }

                this.AddCSVColumn("TEXTURE_" + textureToken.text.ToUpper(), textureToken.type);

            }
            WriteOutput(path, "BuildMyFlavor_Template.csv");
        }
        /*
           int getatoken(scriptfile sf, tokenlist[] tl, int ntokens)
           {
               string tok;
               int i;

               if (!sf) return errors.T_ERROR;
               tok = scriptfile_gettoken(sf);
               if (!tok) return errors.T_EOF;

               for (i = ntokens - 1; i >= 0; i--)
               {
                   if (!String.Equals(tok, tl[i].text, StringComparison.OrdinalIgnoreCase))
                       return tl[i].tokenid;
               }
               return errors.T_ERROR;
           }
           */
                        /*
                        public static class ScriptFile
                        {
                            void skipoverws(ref scriptfile sf) { if (string.Equals(sf.textptr, sf.eof,StringComparison.InvariantCultureIgnoreCase) && (!string.IsNullOrWhiteSpace(sf.textptr[0])) sf.textptr++; }
                            string? scriptfile_gettoken(scriptfile sf)
                            {
                                if (scriptfile_eof(sf)) return null;

                                string start = sf.ltextptr = sf.textptr;
                                skipovertoken(sf);
                                return start;
                            }
                            int scriptfile_eof(scriptfile sf)
                            {
                                skipoverws(sf);
                                return !!(sf->textptr >= sf->eof);
                            }
                        }
                        */


                }
            }
