using System;
using System.IO;

class StowFile
{
  //lastAccess
  //size
  //crc hash
  //fullpath

  //ctor (path

  //get name (local name) function 
  //get fqdn function
  //get directory function
}
class FileCache
{
  //Vector of StowFiles
  //Vector of DiffFiles
  public static int GetDirectoryData(int Directory)
  {
    //get files in directory.
    //if is file
      //vector.push( new stowfile( path, lastaccess )
    //else //is directory
      //getdirectorydata( subdirectory)

    return 0;
  }

public static int GetDirectoryData(int Directory)
  {
    //get files in directory.
    //if is file
      //get crc
      //if crc != vector[blah] vector.push( new stowfile( path, lastaccess )
    //else //is directory
      //getdirectorydata( subdirectory)

    return 0;
  }
  public static void main()
  {
    //audit1
    //Get Directory C:\
  }
}
