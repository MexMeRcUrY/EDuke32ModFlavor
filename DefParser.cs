using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MBlood_ModDocumentation.DefParser;
using static System.Net.Mime.MediaTypeNames;


namespace MBlood_ModDocumentation
{
    public class DefParser
    {
        /*
        public struct scriptfile
        {

            public string textbuf;
            public int textlength;
            public string ltextptr;     // pointer to start of the last token fetched (use this for line numbers)
            public string textptr;
            public string eof;
            public string filename;
            public int linenum;
            public int lineoffs;
        }
        */
        public struct tokenlist
        {
            public string text;
            public scriptTokens tokenid;
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

        tokenlist[] basetokens =
        new tokenlist[]{
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
        public tokenlist getatoken(string script, tokenlist[] tl)
        {

        }
        public void readDEF(string path)
        {
            string scriptFileText = File.ReadAllText(path);
            tokenlist tokn;
            tokn.tokenid = 0;
            switch (tokn.tokenid)
            {
                case scriptTokens.T_ERROR:
                    //LOG_F(ERROR, "%s:%d: unknown error.", script->filename, scriptfile_getlinum(script, cmdtokptr));
                    break;
                case scriptTokens.T_EOF:
                    break;
                case scriptTokens.T_DEFINEMODEL:
                    {
                        string modelfn;
                        double scale;
                        int shadeoffs;

                        //if (scriptfile_getstring(script, &modelfn)) break;
                        //if (scriptfile_getdouble(script, &scale)) break;
                        //if (scriptfile_getnumber(script, &shadeoffs)) break;
                    }
                    break;
            }
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
