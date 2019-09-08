from http.server import HTTPServer, BaseHTTPRequestHandler

from io import BytesIO

import pywaves as pw

# some pw preparations
pw.setNode(node='https://testnodes.wavesnodes.com', chain='testnet')
myAddress = pw.Address(privateKey='')

#auditor
#zavod

t = ""


class SimpleHTTPRequestHandler(BaseHTTPRequestHandler):

    def do_GET(self):
        self.send_response(200)
        self.end_headers()
        #self.wfile.write(b'your path is : ')
        #self.wfile.write(str.encode(self.path))
        
        print(self.path)
        #sys.stdout.flush()

        s = self.path[1: ]
        signature = ""
        t = ""
        
        if (s.find("zavod") >=0):
            print("zavod")
            signature = "Zavod-signature"
            s = s.replace('zavod', '').replace('auditor', '')
            data = [{
                'type':'string', 
                'key': signature, 
                'value':s
            }]
            t = "2"
            myAddress.dataTransaction(data)
            #print('id' + str(t['id']))
            #sys.stdout.flush()
        elif (s.find("auditor") >=0):
            print("auditor")
            signature = "Auditor-signature"
            s = s.replace('zavod', '').replace('auditor', '')
            data = [{
                'type':'string', 
                'key': signature, 
                'value':s
            }]
            t = "1"
            data = myAddress.dataTransaction(data)
            print(t)
            #sys.stdout.flush()
        else:
             print("wrong request")
             #sys.stdout.flush()

        self.wfile.write(str.encode(t))
        
    def do_POST(self):
        content_length = int(self.headers['Content-Length'])
        body = self.rfile.read(content_length)
        self.send_response(200)
        self.end_headers()
        response = BytesIO()
        response.write(b'POST request. ')
        response.write(b'Received: ')
        response.write(body)
        self.wfile.write(response.getvalue())

httpd = HTTPServer(('', 8087), SimpleHTTPRequestHandler)
httpd.serve_forever()
