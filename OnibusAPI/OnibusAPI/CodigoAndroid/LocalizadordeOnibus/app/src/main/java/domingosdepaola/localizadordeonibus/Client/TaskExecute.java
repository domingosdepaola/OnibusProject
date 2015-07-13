package domingosdepaola.localizadordeonibus.Client;

import android.os.AsyncTask;

import java.util.List;
import java.util.Objects;

import domingosdepaola.localizadordeonibus.BO.Onibus;
import domingosdepaola.localizadordeonibus.OnibusActivity;

/**
 * Created by Doo on 11/07/2015.
 */
public class TaskExecute extends AsyncTask {
    public AsyncResponse delegate=null;
    public String numeroLinha;
    private OnibusActivity activity;
    public TaskExecute(OnibusActivity activity) {
        this.activity = activity;
    }
    public TaskExecute(){

    }
    @Override
    protected void onPostExecute(Object result) {
        delegate.processFinish(result);
    }

    @Override
    protected Object doInBackground(Object[] params) {
        OnibusClient onibusClient = new OnibusClient();
        String numeroLinha = "";
        if(params.length > 0){

            numeroLinha = params[0].toString();
        }
        return onibusClient.getOnibus(numeroLinha);
    }
}
