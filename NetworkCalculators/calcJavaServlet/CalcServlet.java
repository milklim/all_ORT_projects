import java.io.*;
import javax.servlet.*;
import javax.servlet.http.*;

public class CalcServlet extends HttpServlet {

    private Double num1, num2, res;
    private String op;

    public void doGet(HttpServletRequest request, HttpServletResponse response)
      throws ServletException, IOException {
      response.setContentType("text/plain");

      
      String origin = request.getHeader("Origin");
      response.addHeader("Access-Control-Allow-Origin", origin == null ? "*" : origin);

      num1 = Double.parseDouble(request.getParameter("num1"));
      num2 = Double.parseDouble(request.getParameter("num2"));
      op = request.getParameter("opr");

      switch (op)
      {
          case "+":
          case "plus":
              res = num1 + num2;
              break;
          case "-":
              res = num1 - num2;
              break;
          case "/":
              res = num1 / num2;
              break;
          case "*":
              res = num1 * num2;
              break;
      }
      PrintWriter out = response.getWriter();       
      out.print(res.toString());
   }

    public void doPost(HttpServletRequest request, HttpServletResponse response)
      throws ServletException, IOException {
      doGet(request, response);
   }
}