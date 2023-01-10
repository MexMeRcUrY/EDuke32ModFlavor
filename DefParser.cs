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
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MBlood_ModDocumentation.DefParser;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace MBlood_ModDocumentation
{
    public class DefParser
    {
        //CSV Variabkles
        DataTable tblCSVOutput = new DataTable();
        DataRow curRow;
        List<DataColumn> OutputColumns = new List<DataColumn>();
        public static string SYSCHAR = "~";
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
        tokenlist[] modelskintokens =
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
                        new tokenlist(){ text =  "alphacut",tokenid =scriptTokens.T_ALPHACUT },
                        new tokenlist(){ text =  "detailscale",tokenid = scriptTokens.T_XSCALE }, new tokenlist(){  text ="scale",  tokenid = scriptTokens.T_XSCALE }, new tokenlist(){  text ="xscale",  tokenid = scriptTokens.T_XSCALE }, new tokenlist(){  text ="intensity",  tokenid = scriptTokens.T_XSCALE },
                        new tokenlist(){ text =  "yscale",tokenid =scriptTokens.T_YSCALE },
                        new tokenlist(){ text =  "specpower",tokenid =scriptTokens.T_SPECPOWER }, new tokenlist(){ text = "specularpower",  tokenid =scriptTokens.T_SPECPOWER }, new tokenlist(){  text ="parallaxscale",  tokenid =scriptTokens.T_SPECPOWER },
                        new tokenlist(){ text =  "specfactor",tokenid =scriptTokens.T_SPECFACTOR }, new tokenlist(){ text = "specularfactor",  tokenid =scriptTokens.T_SPECFACTOR }, new tokenlist(){  text ="parallaxbias",  tokenid =scriptTokens.T_SPECFACTOR },
                        new tokenlist(){ text =  "nocompress",tokenid =scriptTokens.T_NOCOMPRESS },
                        new tokenlist(){ text =  "nodownsize",tokenid =scriptTokens.T_NODOWNSIZE },
                        new tokenlist(){ text =  "forcefilter",tokenid =scriptTokens.T_FORCEFILTER },
                        new tokenlist(){ text =  "artquality",tokenid =scriptTokens.T_ARTQUALITY },
                        new tokenlist(){ text =  "indexed",   tokenid =scriptTokens.T_INDEXED    },
                        new tokenlist(){ text =  "orig_sizex",tokenid =scriptTokens.T_ORIGSIZEX }, new tokenlist(){ text ="orig_sizey",  tokenid =scriptTokens.T_ORIGSIZEY }
        };
        //public tokenlist getatoken(string script, tokenlist[] tl)
        //{
        //    return new tokenlist();
        //}
        public void ReadDEF(string path, out StringBuilder errors)
        {
            errors = errorMsg;

            try
            {
                tokenlist tokn;
                string scriptFileText = File.ReadAllText(path);
                List<string> scriptTextList = scriptFileText.Split(new string[] { "}", "{", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                scriptTextList.RemoveAll(x => x.StartsWith("\r") || x.StartsWith("\n") || x.Trim() == String.Empty);
                string[] scriptText = scriptTextList.ToArray();
                //create
                string modelFileName = String.Empty;
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
                        modelFileName = Path.GetFileName(result);
                        WriteCSVRow("FILENAME", modelFileName, true);
                        WriteCSVRow("TYPE", "Model"); //default
                        WriteCSVRow("TILENUMBER", "-1"); // -1 is just a default that will get replaced once the tile number is read from the script text file
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
                            //if (string.Equals(scriptText_ModelTkns[i], scriptModelSubTkn, StringComparison.OrdinalIgnoreCase))
                            //{
                            scriptTokens foundToken = getToken(scriptText_ModelTkns[i], modeltokens);
                            // switch (scriptModelSubTkn.ToLower())
                            switch (foundToken)
                            {
                                case scriptTokens.T_SKIN:
                                    scriptText_ModelTkns = scriptText[s + 1].Split(" ").ToList();
                                    scriptText_ModelTkns.RemoveAll(x => x.Trim() == String.Empty);
                                    bool newPallet = false;
                                    s++;
                                    // for (int j = 0; j < modelskintokens.Count(); j++)
                                    // {
                                    for (int m = i; m < scriptText_ModelTkns.Count; m++)
                                    {
                                        var foundsubToken = getToken(scriptText_ModelTkns[m], modelskintokens);
                                        switch (foundsubToken)
                                        {
                                            case scriptTokens.T_PAL:
                                                if (newPallet)
                                                {
                                                    cloneCSVRow();
                                                }
                                                WriteCSVRow("PALLETNUMBER", scriptText_ModelTkns[m + 1]);

                                                newPallet = true;
                                                break;
                                            case scriptTokens.T_FILE:
                                                string skinFileName = Path.GetFileName(scriptText_ModelTkns[m + 1]).Replace("\"", String.Empty);
                                                WriteCSVRow("MODEL_SKIN_FILENAME", skinFileName);
                                                break;
                                        }

                                    }
                                    //}
                                    break;
                                case scriptTokens.T_FRAME:
                                    scriptText_ModelTkns = scriptText[s + 1].Split(" ").ToList();
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
                                                updateClonedCSVRows("TILENUMBER", "-1", "TILENUMBER", scriptText_ModelTkns[m + 1]);
                                                break;
                                            case scriptTokens.T_ERROR:
                                                continue;
                                            default:
                                                string tokenFrameText = modeltokens.Where(x => x.tokenid.Equals(foundToken)).FirstOrDefault().text.ToUpper();
                                                WriteCSVRow("MODEL_FRAME_"+ tokenFrameText, scriptText_ModelTkns[i + 1]);
                                                break;
                                        }
                                    }
                                    break;
                                case scriptTokens.T_ERROR:
                                    //token not found becuase its a token value
                                    continue;

                                default:
                                    string tokenText = modeltokens.Where(x => x.tokenid.Equals(foundToken)).FirstOrDefault().text.ToUpper();
                                    WriteCSVRow("MODEL_" + tokenText, scriptText_ModelTkns[i + 1]);
                                    break;
                            }
                            // }

                        }
                    }
                    catch (Exception ex)
                    {
                        errorMsg.AppendLine(" Error Reading Token: " + scriptText_ModelTkns + " -> " + ex.Message);
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
        public scriptTokens getToken(string scriptText, tokenlist[] scriptTokens)
        {
            scriptText = scriptText.Trim();
            var foundToken = scriptTokens.Where(x => scriptText.Equals(x.text, StringComparison.OrdinalIgnoreCase)).Select(x => x.tokenid);
            if (foundToken != null && foundToken.Any())
            {
                return foundToken.FirstOrDefault();
            }
            return DefParser.scriptTokens.T_ERROR;

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

            if (isNumeric)
                AddCSVColumn(columnName, typeof(float));
            else
                AddCSVColumn(columnName, typeof(string));
            if (newRow)
            {
                curRow = tblCSVOutput.NewRow();
                tblCSVOutput.Rows.Add(curRow);
            }
            if (isNumeric)
                curRow[columnName] = fValue;
            else
                curRow[columnName] = value;
        }
        private void cloneCSVRow()
        {
            if (curRow != null)
            {
                DataRow desRow = tblCSVOutput.NewRow();
                desRow.ItemArray = curRow.ItemArray.Clone() as object[];
                tblCSVOutput.Rows.Add(desRow);
                curRow = desRow;
            }
        }
        private void updateClonedCSVRows(string column, string columnKey, string updColumnName, string updValue)
        {
            if (tblCSVOutput.Columns != null && tblCSVOutput.Columns.Count > 0)
            {
                DataColumn col = tblCSVOutput.Columns[column];
                int intValue = 0;
                bool isNumeric = int.TryParse(columnKey, out intValue);
                string sValue = string.Empty;
                if (isNumeric)
                {
                    sValue = intValue.ToString();
                }else
                {
                    sValue = columnKey;
                }

                foreach(DataRow row in tblCSVOutput.Rows)
                {
                    if (row[col].ToString().Equals( sValue, StringComparison.OrdinalIgnoreCase))
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
