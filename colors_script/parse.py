from bs4 import BeautifulSoup
from pprint import pprint
soup = BeautifulSoup(open('colors.html'), 'html.parser')

colors = []
allP = soup.find_all('p')[3:]
usefulP = []
for p in allP:
  if p.get('title') or (len(p.contents) and str(p.contents[0]) != '<br/>' and str(p.contents[0]) != '] '):
    if (len(p.contents) and str(p.contents[0]) != '<br/>' and str(p.contents[0])[0] != '['):
      print(p.contents)
    usefulP.append(p)

for i in range(0, len(usefulP), 2):
  title = usefulP[i].get('title')
  if not title:
    continue
  rgb = title.split(';')[1][5:][:-1]
  rgb = [int(c)/255.0 for c in rgb.split(',')]
  if not len(usefulP[i+1].contents):
    continue
  contents = usefulP[i+1].contents[0]
  if usefulP[i+1].find('a') is not None:
    contents = usefulP[i+1].find('a').contents[0]
  contents = ''.join(e for e in contents if e.isalnum())
  colors.append({'name':contents, 'rgb':rgb})
  #rgb, contents
outfile = open('GameColors.cs', 'w')
for color in colors:
  outfile.write('public static Color %s = new Vector4(%.3ff, %.3ff, %.3ff, 1);\n' % (color['name'], color['rgb'][0], color['rgb'][1], color['rgb'][2]))