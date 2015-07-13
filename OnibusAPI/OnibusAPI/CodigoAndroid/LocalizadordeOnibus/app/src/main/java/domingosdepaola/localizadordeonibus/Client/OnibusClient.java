package domingosdepaola.localizadordeonibus.Client;

import java.util.Arrays;
import java.util.List;

import domingosdepaola.localizadordeonibus.BO.Onibus;


/**
 * Created by Doo on 11/07/2015.
 */
public class OnibusClient extends BaseClient {
    public static final String CONTROLLER = "onibus";
    public String url = BaseClient.ROOT_URL + "/" + this.CONTROLLER;

    public List<Onibus> getOnibus(String numeroLinha){

        try {

            //Onibus[] listOnibus = super.get(url.concat("/getOnibus?numeroLinha=" + numeroLinha + "&x={x}&y={y}"), super.getDefaultParameters(token, key), Onibus[].class, key);
            Onibus[] listOnibus = super.getOpen(url.concat("/getonibus?numeroLinha={numeroLinha}"), super.getParameter("numeroLinha",numeroLinha), Onibus[].class);

            return Arrays.asList(listOnibus);
        }
        catch (Exception e) {

            //logar
        }

        return null;
    }
    public String getOnibusJson(String numeroLinha){

        try {

            String retornoJson = super.getJsonOpen(url.concat("/getonibus?numeroLinha={numeroLinha}"), super.getParameter("numeroLinha",numeroLinha));

            return retornoJson;
        }
        catch (Exception e) {

            //logar
        }

        return null;
    }
}
