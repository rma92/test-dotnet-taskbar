#include <windows.h>
#include <stdio.h>

void setWorkArea()
{
  RECT workarea;
  SystemParametersInfo( SPI_GETWORKAREA, 0, &workarea, 0);
  workarea.left = 0;
  workarea.top = 0;
  workarea.right = 1366;
  workarea.bottom = 768;
  SystemParametersInfo( SPI_SETWORKAREA, 0, &workarea, SPIF_SENDCHANGE);
  printf("%d %d %d %d", workarea.left, workarea.top, workarea.right, workarea.bottom);
}

void adjustMinimiedSize()
{
  MINIMIZEDMETRICS mm;
  memset( &mm, 0, sizeof( MINIMIZEDMETRICS ) );
  mm.cbSize = sizeof( MINIMIZEDMETRICS );
  SystemParametersInfo( SPI_GETMINIMIZEDMETRICS,
                      sizeof( MINIMIZEDMETRICS ),
                      &mm,
                      FALSE);
  printf("Width: %d HorzGap: %d VertGap: %d Arrange: %d", mm.iWidth, mm.iHorzGap, mm.iVertGap, mm.iArrange );
  //W10 default: 154 0 0 0
  mm.iWidth = 240;
  mm.iHorzGap = 0;
  mm.iVertGap = 0;
  mm.iArrange = 0; //
  SystemParametersInfo( SPI_SETMINIMIZEDMETRICS,
                      sizeof( MINIMIZEDMETRICS ),
                      &mm,
                      SPIF_SENDCHANGE);
}
void setMinimizedState()
{
  MINIMIZEDMETRICS mm;
  memset( &mm, 0, sizeof( MINIMIZEDMETRICS ) );
  mm.cbSize = sizeof( MINIMIZEDMETRICS );
  mm.iWidth = 240;
  mm.iHorzGap = 0;
  mm.iVertGap = 0;
  mm.iArrange = ARW_HIDE; 
  SystemParametersInfo( SPI_SETMINIMIZEDMETRICS,
                      sizeof( MINIMIZEDMETRICS ),
                      &mm,
                      SPIF_SENDCHANGE);
}

int main()
{
//set the working area
  setMinimizedState();
}
