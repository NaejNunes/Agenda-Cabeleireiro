using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] protected Text txtDataNumeria, txtDataEscrita, txtsemana;
    [SerializeField] protected GameObject objPanelCliente;
    [SerializeField] protected Button buttonAnterior;

    [SerializeField] protected GameObject[] objPanelHora;
    [SerializeField] protected string[] txtNomeCliente, txtTelefone, txtServico, txtObservacao;
    [SerializeField] protected Text[] txtButtonCliente;
    [SerializeField] protected InputField inputNome, inputTelefone, inputServico, inputObservacao;

    private string mes, semanas;
    public int dias = 1, meses = 1, ano = 2021, qtdAnoBissexto = 1, contSemanas = 0, idHoras;
    private bool checkAnoBissexto;

    private void Awake()
    {
        dias = PlayerPrefs.GetInt("DiaPrefs");
        meses = PlayerPrefs.GetInt("MesPrefs");
        ano = PlayerPrefs.GetInt("AnoPrefs");
        contSemanas = PlayerPrefs.GetInt("ContadorSemana");
        qtdAnoBissexto = PlayerPrefs.GetInt("QuantidadeBissexto");

        CarregarInformacoes();
    }

    void Update()
    {
        if (dias <= 9 && meses <= 9)
            txtDataNumeria.text = "0" + dias + "/0" + meses + "/" + ano;
        else if (dias <= 9 && meses >= 9)
            txtDataNumeria.text = "0" + dias + "/" + meses + "/" + ano;
        else if (dias >= 9 && meses <= 9)
            txtDataNumeria.text = "" + dias + "/0" + meses + "/" + ano;
        else
            txtDataNumeria.text = "" + dias + "/" + meses + "/" + ano;

        if (dias <= 9)
            txtDataEscrita.text = "0" + dias + "/" + mes + "/" + ano;
        else
            txtDataEscrita.text = "" + dias + "/" + mes + "/" + ano;

        txtsemana.text = "" + semanas;


        if (meses > 12)
        {
            ano++;
            qtdAnoBissexto++;
            if (qtdAnoBissexto > 4)
            {
                qtdAnoBissexto = 0;
            }
            meses = 1;
        }

        if (qtdAnoBissexto > 4)
        {
            qtdAnoBissexto = 0;
        }
        else if (qtdAnoBissexto == 4)
        {
            checkAnoBissexto = true;
        }
        else if (qtdAnoBissexto < 4)
        {
            checkAnoBissexto = false;
        }

        if (contSemanas > 6)
        {
            contSemanas = 0;
        }
        else if (contSemanas < 0)
        {
            contSemanas = 6;
        }

        if (dias <= 1 && meses <= 1 && ano <= 2021)
        {
            dias = 1;
            meses = 1;
            ano = 2021;

            contSemanas = 0;
        }
        if (dias <= 1 && meses <= 1)
            buttonAnterior.interactable = false;
        else
            buttonAnterior.interactable = true;

        ControladorMes();
        ControladorSemana();
        SalvarInformacoes();
        CarregarDadosCliente();
    }

    public void ButtonAbrirPanelCliente(int id)
    {
        idHoras = id;

        inputNome.text = PlayerPrefs.GetString("NomeCliente" + id + "/" + dias + "");
        inputTelefone.text = PlayerPrefs.GetString("TelefoneCliente" + id + "/" + dias + "");
        inputServico.text = PlayerPrefs.GetString("ServicoCliente" + id + "/" + dias + "");
        inputObservacao.text = PlayerPrefs.GetString("ObservacaoCliente" + id + "/" + dias + "");

        objPanelCliente.SetActive(true);
    }

    public void MarcarHorario()
    {
        txtNomeCliente[idHoras] = inputNome.text;
        txtTelefone[idHoras] = inputTelefone.text;
        txtServico[idHoras] = inputServico.text;
        txtObservacao[idHoras] = inputObservacao.text;

        PlayerPrefs.SetString("NomeCliente" + idHoras + "/" + dias + "", txtNomeCliente[idHoras]);
        PlayerPrefs.SetString("TelefoneCliente" + idHoras + "/" + dias + "", txtTelefone[idHoras]);
        PlayerPrefs.SetString("ServicoCliente" + idHoras + "/" + dias + "", txtServico[idHoras]);
        PlayerPrefs.SetString("ObservacaoCliente" + idHoras + "/" + dias + "", txtObservacao[idHoras]);

        objPanelCliente.SetActive(false);
    }
    public void SalvarInformacoes()
    {
        PlayerPrefs.SetInt("DiaPrefs", dias);
        PlayerPrefs.SetInt("MesPrefs", meses);
        PlayerPrefs.SetInt("AnoPrefs", ano);
        PlayerPrefs.SetInt("ContadorSemana", contSemanas);
        PlayerPrefs.SetInt("QuantidadeBissexto", qtdAnoBissexto);

    }

    public void ButttonProximoDia()
    {
        dias++;
        contSemanas++;

        if (dias >= 30 && meses == 4 || dias >= 30 && meses == 6 || dias >= 30 && meses == 9 || dias >= 30 && meses == 11)
        {
            meses++;
            dias = 0;
        }
        else if (checkAnoBissexto == false && dias >= 28 && meses == 2)
        {
            meses++;
            dias = 0;
        }
        else if (checkAnoBissexto == true && dias >= 29 && meses == 2)
        {
            meses++;
            dias = 0;
        }
        else if (dias >= 31 && meses == 1 || dias >= 31 && meses == 3 || dias >= 31 && meses == 5 || dias >= 31 && meses == 7 || dias >= 31 && meses == 8 || dias >= 31 && meses == 10 || dias >= 31 && meses == 12)
        {
            meses++;
            dias = 0;
        }
    }
    public void ButttonAnteriorDia()
    {
        if (dias <= 1 && meses == 4 || dias <= 1 && meses == 2 || dias <= 0 && meses == 6 || dias <= 0 && meses == 9 || dias <= 0 && meses == 11)
        {
            meses--;
            dias = 32;
        }
        else if (checkAnoBissexto == false && dias <= 1 && meses == 3)
        {
            meses--;
            dias = 29;
        }
        else if (checkAnoBissexto == true && dias <= 1 && meses == 3)
        {
            meses--;
            dias = 30;
        }
        else if (dias <= 1 && meses == 5 || dias <= 1 && meses == 7 || dias <= 1 && meses == 8 || dias <= 1 && meses == 10 || dias <= 1 && meses == 12)
        {
            meses--;
            dias = 31;
        }

        dias--;
        contSemanas--;
    }
    public void ButttonProximoMes()
    {
        meses++;
    }
    public void ButttonAnteriorMes()
    {
        meses--;
    }

    public void ControladorSemana()
    {
        switch (contSemanas)
        {
            case 0:
                semanas = "Sexta-Feira";
                break;

            case 1:
                semanas = "Sabado";
                break;

            case 2:
                semanas = "Domingo";
                break;

            case 3:
                semanas = "Segunda-Feira";
                break;

            case 4:
                semanas = "Terça-Feira";
                break;

            case 5:
                semanas = "Quarta-Feira";
                break;

            case 6:
                semanas = "Quinta-Feira";
                break;

        }
    }
    public void ControladorMes()
    {
        switch (meses)
        {
            case 1:
                mes = "Janeiro";
                break;

            case 2:
                mes = "Fevereiro";
                break;

            case 3:
                mes = "Março";
                break;

            case 4:
                mes = "Abril";
                break;

            case 5:
                mes = "Maio";
                break;

            case 6:
                mes = "Junho";
                break;

            case 7:
                mes = "Julho";
                break;

            case 8:
                mes = "Agosto";
                break;

            case 9:
                mes = "Setembro";
                break;

            case 10:
                mes = "Outubro";
                break;

            case 11:
                mes = "Novembro";
                break;

            case 12:
                mes = "Dezembro";
                break;
        }
    }

    public void CarregarInformacoes()
    {
        txtNomeCliente[0] = "" + PlayerPrefs.GetString("NomeCliente" + 0 + "/" + dias + "");
        txtNomeCliente[1] = "" + PlayerPrefs.GetString("NomeCliente" + 1 + "/" + dias + "");
        txtNomeCliente[2] = "" + PlayerPrefs.GetString("NomeCliente" + 2 + "/" + dias + "");
        txtNomeCliente[3] = "" + PlayerPrefs.GetString("NomeCliente" + 3 + "/" + dias + "");
        txtNomeCliente[4] = "" + PlayerPrefs.GetString("NomeCliente" + 4 + "/" + dias + "");
        txtNomeCliente[5] = "" + PlayerPrefs.GetString("NomeCliente" + 5 + "/" + dias + "");
        txtNomeCliente[6] = "" + PlayerPrefs.GetString("NomeCliente" + 6 + "/" + dias + "");
        txtNomeCliente[7] = "" + PlayerPrefs.GetString("NomeCliente" + 7 + "/" + dias + "");
        txtNomeCliente[8] = "" + PlayerPrefs.GetString("NomeCliente" + 8 + "/" + dias + "");
        txtNomeCliente[9] = "" + PlayerPrefs.GetString("NomeCliente" + 9 + "/" + dias + "");
        txtNomeCliente[10] = "" + PlayerPrefs.GetString("NomeCliente" + 10 + "/" + dias + "");
        txtNomeCliente[11] = "" + PlayerPrefs.GetString("NomeCliente" + 11 + "/" + dias + "");
        txtNomeCliente[12] = "" + PlayerPrefs.GetString("NomeCliente" + 12 + "/" + dias + "");
        txtNomeCliente[13] = "" + PlayerPrefs.GetString("NomeCliente" + 13 + "/" + dias + "");
        txtNomeCliente[14] = "" + PlayerPrefs.GetString("NomeCliente" + 14 + "/" + dias + "");
        txtNomeCliente[15] = "" + PlayerPrefs.GetString("NomeCliente" + 15 + "/" + dias + "");
        txtNomeCliente[16] = "" + PlayerPrefs.GetString("NomeCliente" + 16 + "/" + dias + "");
        txtNomeCliente[17] = "" + PlayerPrefs.GetString("NomeCliente" + 17 + "/" + dias + "");
        txtNomeCliente[18] = "" + PlayerPrefs.GetString("NomeCliente" + 18 + "/" + dias + "");
        txtNomeCliente[19] = "" + PlayerPrefs.GetString("NomeCliente" + 19 + "/" + dias + "");
        txtNomeCliente[20] = "" + PlayerPrefs.GetString("NomeCliente" + 20 + "/" + dias + "");
        txtNomeCliente[21] = "" + PlayerPrefs.GetString("NomeCliente" + 21 + "/" + dias + "");
        txtNomeCliente[22] = "" + PlayerPrefs.GetString("NomeCliente" + 22 + "/" + dias + "");
        txtNomeCliente[23] = "" + PlayerPrefs.GetString("NomeCliente" + 23 + "/" + dias + "");
        txtNomeCliente[24] = "" + PlayerPrefs.GetString("NomeCliente" + 24 + "/" + dias + "");

        txtNomeCliente[25] = "" + PlayerPrefs.GetString("NomeCliente" + 25 + "/" + dias + "");
        txtNomeCliente[26] = "" + PlayerPrefs.GetString("NomeCliente" + 26 + "/" + dias + "");
        txtNomeCliente[27] = "" + PlayerPrefs.GetString("NomeCliente" + 27 + "/" + dias + "");
        txtNomeCliente[28] = "" + PlayerPrefs.GetString("NomeCliente" + 28 + "/" + dias + "");
        txtNomeCliente[29] = "" + PlayerPrefs.GetString("NomeCliente" + 29 + "/" + dias + "");
        txtNomeCliente[30] = "" + PlayerPrefs.GetString("NomeCliente" + 30 + "/" + dias + "");
        txtNomeCliente[31] = "" + PlayerPrefs.GetString("NomeCliente" + 31 + "/" + dias + "");
        txtNomeCliente[32] = "" + PlayerPrefs.GetString("NomeCliente" + 32 + "/" + dias + "");
        txtNomeCliente[33] = "" + PlayerPrefs.GetString("NomeCliente" + 33 + "/" + dias + "");
        txtNomeCliente[34] = "" + PlayerPrefs.GetString("NomeCliente" + 34 + "/" + dias + "");
        txtNomeCliente[35] = "" + PlayerPrefs.GetString("NomeCliente" + 35 + "/" + dias + "");
        txtNomeCliente[36] = "" + PlayerPrefs.GetString("NomeCliente" + 36 + "/" + dias + "");
        txtNomeCliente[37] = "" + PlayerPrefs.GetString("NomeCliente" + 37 + "/" + dias + "");
        txtNomeCliente[38] = "" + PlayerPrefs.GetString("NomeCliente" + 38 + "/" + dias + "");
        txtNomeCliente[39] = "" + PlayerPrefs.GetString("NomeCliente" + 39 + "/" + dias + "");
        txtNomeCliente[40] = "" + PlayerPrefs.GetString("NomeCliente" + 40 + "/" + dias + "");
        txtNomeCliente[41] = "" + PlayerPrefs.GetString("NomeCliente" + 41 + "/" + dias + "");
        txtNomeCliente[42] = "" + PlayerPrefs.GetString("NomeCliente" + 42 + "/" + dias + "");
        txtNomeCliente[43] = "" + PlayerPrefs.GetString("NomeCliente" + 43 + "/" + dias + "");
        txtNomeCliente[44] = "" + PlayerPrefs.GetString("NomeCliente" + 44 + "/" + dias + "");
        txtNomeCliente[45] = "" + PlayerPrefs.GetString("NomeCliente" + 45 + "/" + dias + "");
        txtNomeCliente[46] = "" + PlayerPrefs.GetString("NomeCliente" + 46 + "/" + dias + "");
        txtNomeCliente[47] = "" + PlayerPrefs.GetString("NomeCliente" + 47 + "/" + dias + "");
        txtNomeCliente[48] = "" + PlayerPrefs.GetString("NomeCliente" + 48 + "/" + dias + "");
        txtNomeCliente[49] = "" + PlayerPrefs.GetString("NomeCliente" + 49 + "/" + dias + "");

        txtNomeCliente[50] = "" + PlayerPrefs.GetString("NomeCliente" + 50 + "/" + dias + "");
        txtNomeCliente[51] = "" + PlayerPrefs.GetString("NomeCliente" + 51 + "/" + dias + "");
        txtNomeCliente[52] = "" + PlayerPrefs.GetString("NomeCliente" + 52 + "/" + dias + "");
        txtNomeCliente[53] = "" + PlayerPrefs.GetString("NomeCliente" + 53 + "/" + dias + "");
        txtNomeCliente[54] = "" + PlayerPrefs.GetString("NomeCliente" + 54 + "/" + dias + "");
        txtNomeCliente[55] = "" + PlayerPrefs.GetString("NomeCliente" + 55 + "/" + dias + "");
        txtNomeCliente[56] = "" + PlayerPrefs.GetString("NomeCliente" + 56 + "/" + dias + "");
        txtNomeCliente[57] = "" + PlayerPrefs.GetString("NomeCliente" + 57 + "/" + dias + "");
        txtNomeCliente[58] = "" + PlayerPrefs.GetString("NomeCliente" + 58 + "/" + dias + "");
        txtNomeCliente[59] = "" + PlayerPrefs.GetString("NomeCliente" + 59 + "/" + dias + "");
        txtNomeCliente[60] = "" + PlayerPrefs.GetString("NomeCliente" + 60 + "/" + dias + "");
        txtNomeCliente[61] = "" + PlayerPrefs.GetString("NomeCliente" + 61 + "/" + dias + "");
        txtNomeCliente[62] = "" + PlayerPrefs.GetString("NomeCliente" + 62 + "/" + dias + "");
        txtNomeCliente[63] = "" + PlayerPrefs.GetString("NomeCliente" + 63 + "/" + dias + "");
        txtNomeCliente[64] = "" + PlayerPrefs.GetString("NomeCliente" + 64 + "/" + dias + "");
        txtNomeCliente[65] = "" + PlayerPrefs.GetString("NomeCliente" + 65 + "/" + dias + "");
        txtNomeCliente[66] = "" + PlayerPrefs.GetString("NomeCliente" + 66 + "/" + dias + "");
        txtNomeCliente[67] = "" + PlayerPrefs.GetString("NomeCliente" + 67 + "/" + dias + "");
        txtNomeCliente[68] = "" + PlayerPrefs.GetString("NomeCliente" + 68 + "/" + dias + "");
        txtNomeCliente[69] = "" + PlayerPrefs.GetString("NomeCliente" + 69 + "/" + dias + "");
        txtNomeCliente[70] = "" + PlayerPrefs.GetString("NomeCliente" + 70 + "/" + dias + "");
        txtNomeCliente[71] = "" + PlayerPrefs.GetString("NomeCliente" + 71 + "/" + dias + "");
        txtNomeCliente[72] = "" + PlayerPrefs.GetString("NomeCliente" + 72 + "/" + dias + "");
        txtNomeCliente[73] = "" + PlayerPrefs.GetString("NomeCliente" + 73 + "/" + dias + "");
        txtNomeCliente[74] = "" + PlayerPrefs.GetString("NomeCliente" + 74 + "/" + dias + "");

        txtNomeCliente[75] = "" + PlayerPrefs.GetString("NomeCliente" + 75 + "/" + dias + "");
        txtNomeCliente[76] = "" + PlayerPrefs.GetString("NomeCliente" + 76 + "/" + dias + "");
        txtNomeCliente[77] = "" + PlayerPrefs.GetString("NomeCliente" + 77 + "/" + dias + "");
        txtNomeCliente[78] = "" + PlayerPrefs.GetString("NomeCliente" + 78 + "/" + dias + "");
        txtNomeCliente[79] = "" + PlayerPrefs.GetString("NomeCliente" + 79 + "/" + dias + "");
        txtNomeCliente[80] = "" + PlayerPrefs.GetString("NomeCliente" + 80 + "/" + dias + "");
        txtNomeCliente[81] = "" + PlayerPrefs.GetString("NomeCliente" + 81 + "/" + dias + "");
        txtNomeCliente[82] = "" + PlayerPrefs.GetString("NomeCliente" + 82 + "/" + dias + "");
        txtNomeCliente[83] = "" + PlayerPrefs.GetString("NomeCliente" + 83 + "/" + dias + "");
        txtNomeCliente[84] = "" + PlayerPrefs.GetString("NomeCliente" + 84 + "/" + dias + "");
        txtNomeCliente[85] = "" + PlayerPrefs.GetString("NomeCliente" + 85 + "/" + dias + "");
        txtNomeCliente[86] = "" + PlayerPrefs.GetString("NomeCliente" + 86 + "/" + dias + "");
        txtNomeCliente[87] = "" + PlayerPrefs.GetString("NomeCliente" + 87 + "/" + dias + "");
        txtNomeCliente[88] = "" + PlayerPrefs.GetString("NomeCliente" + 88 + "/" + dias + "");
        txtNomeCliente[89] = "" + PlayerPrefs.GetString("NomeCliente" + 89 + "/" + dias + "");
        txtNomeCliente[90] = "" + PlayerPrefs.GetString("NomeCliente" + 90 + "/" + dias + "");
        txtNomeCliente[91] = "" + PlayerPrefs.GetString("NomeCliente" + 91 + "/" + dias + "");
        txtNomeCliente[92] = "" + PlayerPrefs.GetString("NomeCliente" + 92 + "/" + dias + "");
        txtNomeCliente[93] = "" + PlayerPrefs.GetString("NomeCliente" + 93 + "/" + dias + "");
        txtNomeCliente[94] = "" + PlayerPrefs.GetString("NomeCliente" + 94 + "/" + dias + "");
        txtNomeCliente[95] = "" + PlayerPrefs.GetString("NomeCliente" + 95 + "/" + dias + "");
        txtNomeCliente[96] = "" + PlayerPrefs.GetString("NomeCliente" + 96 + "/" + dias + "");
        txtNomeCliente[97] = "" + PlayerPrefs.GetString("NomeCliente" + 97 + "/" + dias + "");
        txtNomeCliente[98] = "" + PlayerPrefs.GetString("NomeCliente" + 98 + "/" + dias + "");
        txtNomeCliente[99] = "" + PlayerPrefs.GetString("NomeCliente" + 99 + "/" + dias + "");

        txtTelefone[0] = "" + PlayerPrefs.GetString("TelefoneCliente" + 0 + "/" + dias + "");
        txtTelefone[1] = "" + PlayerPrefs.GetString("TelefoneCliente" + 1 + "/" + dias + "");
        txtTelefone[2] = "" + PlayerPrefs.GetString("TelefoneCliente" + 2 + "/" + dias + "");
        txtTelefone[3] = "" + PlayerPrefs.GetString("TelefoneCliente" + 3 + "/" + dias + "");
        txtTelefone[4] = "" + PlayerPrefs.GetString("TelefoneCliente" + 4 + "/" + dias + "");
        txtTelefone[5] = "" + PlayerPrefs.GetString("TelefoneCliente" + 5 + "/" + dias + "");
        txtTelefone[6] = "" + PlayerPrefs.GetString("TelefoneCliente" + 6 + "/" + dias + "");
        txtTelefone[7] = "" + PlayerPrefs.GetString("TelefoneCliente" + 7 + "/" + dias + "");
        txtTelefone[8] = "" + PlayerPrefs.GetString("TelefoneCliente" + 8 + "/" + dias + "");
        txtTelefone[9] = "" + PlayerPrefs.GetString("TelefoneCliente" + 9 + "/" + dias + "");
        txtTelefone[10] = "" + PlayerPrefs.GetString("TelefoneCliente" + 10 + "/" + dias + "");
        txtTelefone[11] = "" + PlayerPrefs.GetString("TelefoneCliente" + 11 + "/" + dias + "");
        txtTelefone[12] = "" + PlayerPrefs.GetString("TelefoneCliente" + 12 + "/" + dias + "");
        txtTelefone[13] = "" + PlayerPrefs.GetString("TelefoneCliente" + 13 + "/" + dias + "");
        txtTelefone[14] = "" + PlayerPrefs.GetString("TelefoneCliente" + 14 + "/" + dias + "");
        txtTelefone[15] = "" + PlayerPrefs.GetString("TelefoneCliente" + 15 + "/" + dias + "");
        txtTelefone[16] = "" + PlayerPrefs.GetString("TelefoneCliente" + 16 + "/" + dias + "");
        txtTelefone[17] = "" + PlayerPrefs.GetString("TelefoneCliente" + 17 + "/" + dias + "");
        txtTelefone[18] = "" + PlayerPrefs.GetString("TelefoneCliente" + 18 + "/" + dias + "");
        txtTelefone[19] = "" + PlayerPrefs.GetString("TelefoneCliente" + 19 + "/" + dias + "");
        txtTelefone[20] = "" + PlayerPrefs.GetString("TelefoneCliente" + 20 + "/" + dias + "");
        txtTelefone[21] = "" + PlayerPrefs.GetString("TelefoneCliente" + 21 + "/" + dias + "");
        txtTelefone[22] = "" + PlayerPrefs.GetString("TelefoneCliente" + 22 + "/" + dias + "");
        txtTelefone[23] = "" + PlayerPrefs.GetString("TelefoneCliente" + 23 + "/" + dias + "");
        txtTelefone[24] = "" + PlayerPrefs.GetString("TelefoneCliente" + 24 + "/" + dias + "");

        txtTelefone[25] = "" + PlayerPrefs.GetString("TelefoneCliente" + 25 + "/" + dias + "");
        txtTelefone[26] = "" + PlayerPrefs.GetString("TelefoneCliente" + 26 + "/" + dias + "");
        txtTelefone[27] = "" + PlayerPrefs.GetString("TelefoneCliente" + 27 + "/" + dias + "");
        txtTelefone[28] = "" + PlayerPrefs.GetString("TelefoneCliente" + 28 + "/" + dias + "");
        txtTelefone[29] = "" + PlayerPrefs.GetString("TelefoneCliente" + 29 + "/" + dias + "");
        txtTelefone[30] = "" + PlayerPrefs.GetString("TelefoneCliente" + 30 + "/" + dias + "");
        txtTelefone[31] = "" + PlayerPrefs.GetString("TelefoneCliente" + 31 + "/" + dias + "");
        txtTelefone[32] = "" + PlayerPrefs.GetString("TelefoneCliente" + 32 + "/" + dias + "");
        txtTelefone[33] = "" + PlayerPrefs.GetString("TelefoneCliente" + 33 + "/" + dias + "");
        txtTelefone[34] = "" + PlayerPrefs.GetString("TelefoneCliente" + 34 + "/" + dias + "");
        txtTelefone[35] = "" + PlayerPrefs.GetString("TelefoneCliente" + 35 + "/" + dias + "");
        txtTelefone[36] = "" + PlayerPrefs.GetString("TelefoneCliente" + 36 + "/" + dias + "");
        txtTelefone[37] = "" + PlayerPrefs.GetString("TelefoneCliente" + 37 + "/" + dias + "");
        txtTelefone[38] = "" + PlayerPrefs.GetString("TelefoneCliente" + 38 + "/" + dias + "");
        txtTelefone[39] = "" + PlayerPrefs.GetString("TelefoneCliente" + 39 + "/" + dias + "");
        txtTelefone[40] = "" + PlayerPrefs.GetString("TelefoneCliente" + 40 + "/" + dias + "");
        txtTelefone[41] = "" + PlayerPrefs.GetString("TelefoneCliente" + 41 + "/" + dias + "");
        txtTelefone[42] = "" + PlayerPrefs.GetString("TelefoneCliente" + 42 + "/" + dias + "");
        txtTelefone[43] = "" + PlayerPrefs.GetString("TelefoneCliente" + 43 + "/" + dias + "");
        txtTelefone[44] = "" + PlayerPrefs.GetString("TelefoneCliente" + 44 + "/" + dias + "");
        txtTelefone[45] = "" + PlayerPrefs.GetString("TelefoneCliente" + 45 + "/" + dias + "");
        txtTelefone[46] = "" + PlayerPrefs.GetString("TelefoneCliente" + 46 + "/" + dias + "");
        txtTelefone[47] = "" + PlayerPrefs.GetString("TelefoneCliente" + 47 + "/" + dias + "");
        txtTelefone[48] = "" + PlayerPrefs.GetString("TelefoneCliente" + 48 + "/" + dias + "");
        txtTelefone[49] = "" + PlayerPrefs.GetString("TelefoneCliente" + 49 + "/" + dias + "");

        txtTelefone[50] = "" + PlayerPrefs.GetString("TelefoneCliente" + 50 + "/" + dias + "");
        txtTelefone[51] = "" + PlayerPrefs.GetString("TelefoneCliente" + 51 + "/" + dias + "");
        txtTelefone[52] = "" + PlayerPrefs.GetString("TelefoneCliente" + 52 + "/" + dias + "");
        txtTelefone[53] = "" + PlayerPrefs.GetString("TelefoneCliente" + 53 + "/" + dias + "");
        txtTelefone[54] = "" + PlayerPrefs.GetString("TelefoneCliente" + 54 + "/" + dias + "");
        txtTelefone[55] = "" + PlayerPrefs.GetString("TelefoneCliente" + 55 + "/" + dias + "");
        txtTelefone[56] = "" + PlayerPrefs.GetString("TelefoneCliente" + 56 + "/" + dias + "");
        txtTelefone[57] = "" + PlayerPrefs.GetString("TelefoneCliente" + 57 + "/" + dias + "");
        txtTelefone[58] = "" + PlayerPrefs.GetString("TelefoneCliente" + 58 + "/" + dias + "");
        txtTelefone[59] = "" + PlayerPrefs.GetString("TelefoneCliente" + 59 + "/" + dias + "");
        txtTelefone[60] = "" + PlayerPrefs.GetString("TelefoneCliente" + 60 + "/" + dias + "");
        txtTelefone[61] = "" + PlayerPrefs.GetString("TelefoneCliente" + 61 + "/" + dias + "");
        txtTelefone[62] = "" + PlayerPrefs.GetString("TelefoneCliente" + 62 + "/" + dias + "");
        txtTelefone[63] = "" + PlayerPrefs.GetString("TelefoneCliente" + 63 + "/" + dias + "");
        txtTelefone[64] = "" + PlayerPrefs.GetString("TelefoneCliente" + 64 + "/" + dias + "");
        txtTelefone[65] = "" + PlayerPrefs.GetString("TelefoneCliente" + 65 + "/" + dias + "");
        txtTelefone[66] = "" + PlayerPrefs.GetString("TelefoneCliente" + 66 + "/" + dias + "");
        txtTelefone[67] = "" + PlayerPrefs.GetString("TelefoneCliente" + 67 + "/" + dias + "");
        txtTelefone[68] = "" + PlayerPrefs.GetString("TelefoneCliente" + 68 + "/" + dias + "");
        txtTelefone[69] = "" + PlayerPrefs.GetString("TelefoneCliente" + 69 + "/" + dias + "");
        txtTelefone[70] = "" + PlayerPrefs.GetString("TelefoneCliente" + 70 + "/" + dias + "");
        txtTelefone[71] = "" + PlayerPrefs.GetString("TelefoneCliente" + 71 + "/" + dias + "");
        txtTelefone[72] = "" + PlayerPrefs.GetString("TelefoneCliente" + 72 + "/" + dias + "");
        txtTelefone[73] = "" + PlayerPrefs.GetString("TelefoneCliente" + 73 + "/" + dias + "");
        txtTelefone[74] = "" + PlayerPrefs.GetString("TelefoneCliente" + 74 + "/" + dias + "");

        txtTelefone[75] = "" + PlayerPrefs.GetString("TelefoneCliente" + 75 + "/" + dias + "");
        txtTelefone[76] = "" + PlayerPrefs.GetString("TelefoneCliente" + 76 + "/" + dias + "");
        txtTelefone[77] = "" + PlayerPrefs.GetString("TelefoneCliente" + 77 + "/" + dias + "");
        txtTelefone[78] = "" + PlayerPrefs.GetString("TelefoneCliente" + 78 + "/" + dias + "");
        txtTelefone[79] = "" + PlayerPrefs.GetString("TelefoneCliente" + 79 + "/" + dias + "");
        txtTelefone[80] = "" + PlayerPrefs.GetString("TelefoneCliente" + 80 + "/" + dias + "");
        txtTelefone[81] = "" + PlayerPrefs.GetString("TelefoneCliente" + 81 + "/" + dias + "");
        txtTelefone[82] = "" + PlayerPrefs.GetString("TelefoneCliente" + 82 + "/" + dias + "");
        txtTelefone[83] = "" + PlayerPrefs.GetString("TelefoneCliente" + 83 + "/" + dias + "");
        txtTelefone[84] = "" + PlayerPrefs.GetString("TelefoneCliente" + 84 + "/" + dias + "");
        txtTelefone[85] = "" + PlayerPrefs.GetString("TelefoneCliente" + 85 + "/" + dias + "");
        txtTelefone[86] = "" + PlayerPrefs.GetString("TelefoneCliente" + 86 + "/" + dias + "");
        txtTelefone[87] = "" + PlayerPrefs.GetString("TelefoneCliente" + 87 + "/" + dias + "");
        txtTelefone[88] = "" + PlayerPrefs.GetString("TelefoneCliente" + 88 + "/" + dias + "");
        txtTelefone[89] = "" + PlayerPrefs.GetString("TelefoneCliente" + 89 + "/" + dias + "");
        txtTelefone[90] = "" + PlayerPrefs.GetString("TelefoneCliente" + 90 + "/" + dias + "");
        txtTelefone[91] = "" + PlayerPrefs.GetString("TelefoneCliente" + 91 + "/" + dias + "");
        txtTelefone[92] = "" + PlayerPrefs.GetString("TelefoneCliente" + 92 + "/" + dias + "");
        txtTelefone[93] = "" + PlayerPrefs.GetString("TelefoneCliente" + 93 + "/" + dias + "");
        txtTelefone[94] = "" + PlayerPrefs.GetString("TelefoneCliente" + 94 + "/" + dias + "");
        txtTelefone[95] = "" + PlayerPrefs.GetString("TelefoneCliente" + 95 + "/" + dias + "");
        txtTelefone[96] = "" + PlayerPrefs.GetString("TelefoneCliente" + 96 + "/" + dias + "");
        txtTelefone[97] = "" + PlayerPrefs.GetString("TelefoneCliente" + 97 + "/" + dias + "");
        txtTelefone[98] = "" + PlayerPrefs.GetString("TelefoneCliente" + 98 + "/" + dias + "");
        txtTelefone[99] = "" + PlayerPrefs.GetString("TelefoneCliente" + 99 + "/" + dias + "");

        txtServico[0] = "" + PlayerPrefs.GetString("ServicoCliente" + 0 + "/" + dias + "");
        txtServico[1] = "" + PlayerPrefs.GetString("ServicoCliente" + 1 + "/" + dias + "");
        txtServico[2] = "" + PlayerPrefs.GetString("ServicoCliente" + 2 + "/" + dias + "");
        txtServico[3] = "" + PlayerPrefs.GetString("ServicoCliente" + 3 + "/" + dias + "");
        txtServico[4] = "" + PlayerPrefs.GetString("ServicoCliente" + 4 + "/" + dias + "");
        txtServico[5] = "" + PlayerPrefs.GetString("ServicoCliente" + 5 + "/" + dias + "");
        txtServico[6] = "" + PlayerPrefs.GetString("ServicoCliente" + 6 + "/" + dias + "");
        txtServico[7] = "" + PlayerPrefs.GetString("ServicoCliente" + 7 + "/" + dias + "");
        txtServico[8] = "" + PlayerPrefs.GetString("ServicoCliente" + 8 + "/" + dias + "");
        txtServico[9] = "" + PlayerPrefs.GetString("ServicoCliente" + 9 + "/" + dias + "");
        txtServico[10] = "" + PlayerPrefs.GetString("ServicoCliente" + 10 + "/" + dias + "");
        txtServico[11] = "" + PlayerPrefs.GetString("ServicoCliente" + 11 + "/" + dias + "");
        txtServico[12] = "" + PlayerPrefs.GetString("ServicoCliente" + 12 + "/" + dias + "");
        txtServico[13] = "" + PlayerPrefs.GetString("ServicoCliente" + 13 + "/" + dias + "");
        txtServico[14] = "" + PlayerPrefs.GetString("ServicoCliente" + 14 + "/" + dias + "");
        txtServico[15] = "" + PlayerPrefs.GetString("ServicoCliente" + 15 + "/" + dias + "");
        txtServico[16] = "" + PlayerPrefs.GetString("ServicoCliente" + 16 + "/" + dias + "");
        txtServico[17] = "" + PlayerPrefs.GetString("ServicoCliente" + 17 + "/" + dias + "");
        txtServico[18] = "" + PlayerPrefs.GetString("ServicoCliente" + 18 + "/" + dias + "");
        txtServico[19] = "" + PlayerPrefs.GetString("ServicoCliente" + 19 + "/" + dias + "");
        txtServico[20] = "" + PlayerPrefs.GetString("ServicoCliente" + 20 + "/" + dias + "");
        txtServico[21] = "" + PlayerPrefs.GetString("ServicoCliente" + 21 + "/" + dias + "");
        txtServico[22] = "" + PlayerPrefs.GetString("ServicoCliente" + 22 + "/" + dias + "");
        txtServico[23] = "" + PlayerPrefs.GetString("ServicoCliente" + 23 + "/" + dias + "");
        txtServico[24] = "" + PlayerPrefs.GetString("ServicoCliente" + 24 + "/" + dias + "");


        txtServico[25] = "" + PlayerPrefs.GetString("ServicoCliente" + 25 + "/" + dias + "");
        txtServico[26] = "" + PlayerPrefs.GetString("ServicoCliente" + 26 + "/" + dias + "");
        txtServico[27] = "" + PlayerPrefs.GetString("ServicoCliente" + 27 + "/" + dias + "");
        txtServico[28] = "" + PlayerPrefs.GetString("ServicoCliente" + 28 + "/" + dias + "");
        txtServico[29] = "" + PlayerPrefs.GetString("ServicoCliente" + 29 + "/" + dias + "");
        txtServico[30] = "" + PlayerPrefs.GetString("ServicoCliente" + 30 + "/" + dias + "");
        txtServico[31] = "" + PlayerPrefs.GetString("ServicoCliente" + 31 + "/" + dias + "");
        txtServico[32] = "" + PlayerPrefs.GetString("ServicoCliente" + 32 + "/" + dias + "");
        txtServico[33] = "" + PlayerPrefs.GetString("ServicoCliente" + 33 + "/" + dias + "");
        txtServico[34] = "" + PlayerPrefs.GetString("ServicoCliente" + 34 + "/" + dias + "");
        txtServico[35] = "" + PlayerPrefs.GetString("ServicoCliente" + 35 + "/" + dias + "");
        txtServico[36] = "" + PlayerPrefs.GetString("ServicoCliente" + 36 + "/" + dias + "");
        txtServico[37] = "" + PlayerPrefs.GetString("ServicoCliente" + 37 + "/" + dias + "");
        txtServico[38] = "" + PlayerPrefs.GetString("ServicoCliente" + 38 + "/" + dias + "");
        txtServico[39] = "" + PlayerPrefs.GetString("ServicoCliente" + 39 + "/" + dias + "");
        txtServico[40] = "" + PlayerPrefs.GetString("ServicoCliente" + 40 + "/" + dias + "");
        txtServico[41] = "" + PlayerPrefs.GetString("ServicoCliente" + 41 + "/" + dias + "");
        txtServico[42] = "" + PlayerPrefs.GetString("ServicoCliente" + 42 + "/" + dias + "");
        txtServico[43] = "" + PlayerPrefs.GetString("ServicoCliente" + 43 + "/" + dias + "");
        txtServico[44] = "" + PlayerPrefs.GetString("ServicoCliente" + 44 + "/" + dias + "");
        txtServico[45] = "" + PlayerPrefs.GetString("ServicoCliente" + 45 + "/" + dias + "");
        txtServico[46] = "" + PlayerPrefs.GetString("ServicoCliente" + 46 + "/" + dias + "");
        txtServico[47] = "" + PlayerPrefs.GetString("ServicoCliente" + 47 + "/" + dias + "");
        txtServico[48] = "" + PlayerPrefs.GetString("ServicoCliente" + 48 + "/" + dias + "");
        txtServico[49] = "" + PlayerPrefs.GetString("ServicoCliente" + 49 + "/" + dias + "");

        txtServico[50] = "" + PlayerPrefs.GetString("ServicoCliente" + 50 + "/" + dias + "");
        txtServico[51] = "" + PlayerPrefs.GetString("ServicoCliente" + 51 + "/" + dias + "");
        txtServico[52] = "" + PlayerPrefs.GetString("ServicoCliente" + 52 + "/" + dias + "");
        txtServico[53] = "" + PlayerPrefs.GetString("ServicoCliente" + 53 + "/" + dias + "");
        txtServico[54] = "" + PlayerPrefs.GetString("ServicoCliente" + 54 + "/" + dias + "");
        txtServico[55] = "" + PlayerPrefs.GetString("ServicoCliente" + 55 + "/" + dias + "");
        txtServico[56] = "" + PlayerPrefs.GetString("ServicoCliente" + 56 + "/" + dias + "");
        txtServico[57] = "" + PlayerPrefs.GetString("ServicoCliente" + 57 + "/" + dias + "");
        txtServico[58] = "" + PlayerPrefs.GetString("ServicoCliente" + 58 + "/" + dias + "");
        txtServico[59] = "" + PlayerPrefs.GetString("ServicoCliente" + 59 + "/" + dias + "");
        txtServico[60] = "" + PlayerPrefs.GetString("ServicoCliente" + 60 + "/" + dias + "");
        txtServico[61] = "" + PlayerPrefs.GetString("ServicoCliente" + 61 + "/" + dias + "");
        txtServico[62] = "" + PlayerPrefs.GetString("ServicoCliente" + 62 + "/" + dias + "");
        txtServico[63] = "" + PlayerPrefs.GetString("ServicoCliente" + 63 + "/" + dias + "");
        txtServico[64] = "" + PlayerPrefs.GetString("ServicoCliente" + 64 + "/" + dias + "");
        txtServico[65] = "" + PlayerPrefs.GetString("ServicoCliente" + 65 + "/" + dias + "");
        txtServico[66] = "" + PlayerPrefs.GetString("ServicoCliente" + 66 + "/" + dias + "");
        txtServico[67] = "" + PlayerPrefs.GetString("ServicoCliente" + 67 + "/" + dias + "");
        txtServico[68] = "" + PlayerPrefs.GetString("ServicoCliente" + 68 + "/" + dias + "");
        txtServico[69] = "" + PlayerPrefs.GetString("ServicoCliente" + 69 + "/" + dias + "");
        txtServico[70] = "" + PlayerPrefs.GetString("ServicoCliente" + 70 + "/" + dias + "");
        txtServico[71] = "" + PlayerPrefs.GetString("ServicoCliente" + 71 + "/" + dias + "");
        txtServico[72] = "" + PlayerPrefs.GetString("ServicoCliente" + 72 + "/" + dias + "");
        txtServico[73] = "" + PlayerPrefs.GetString("ServicoCliente" + 73 + "/" + dias + "");
        txtServico[74] = "" + PlayerPrefs.GetString("ServicoCliente" + 74 + "/" + dias + "");

        txtServico[75] = "" + PlayerPrefs.GetString("ServicoCliente" + 75 + "/" + dias + "");
        txtServico[76] = "" + PlayerPrefs.GetString("ServicoCliente" + 76 + "/" + dias + "");
        txtServico[77] = "" + PlayerPrefs.GetString("ServicoCliente" + 77 + "/" + dias + "");
        txtServico[78] = "" + PlayerPrefs.GetString("ServicoCliente" + 78 + "/" + dias + "");
        txtServico[79] = "" + PlayerPrefs.GetString("ServicoCliente" + 79 + "/" + dias + "");
        txtServico[80] = "" + PlayerPrefs.GetString("ServicoCliente" + 80 + "/" + dias + "");
        txtServico[81] = "" + PlayerPrefs.GetString("ServicoCliente" + 81 + "/" + dias + "");
        txtServico[82] = "" + PlayerPrefs.GetString("ServicoCliente" + 82 + "/" + dias + "");
        txtServico[83] = "" + PlayerPrefs.GetString("ServicoCliente" + 83 + "/" + dias + "");
        txtServico[84] = "" + PlayerPrefs.GetString("ServicoCliente" + 84 + "/" + dias + "");
        txtServico[85] = "" + PlayerPrefs.GetString("ServicoCliente" + 85 + "/" + dias + "");
        txtServico[86] = "" + PlayerPrefs.GetString("ServicoCliente" + 86 + "/" + dias + "");
        txtServico[87] = "" + PlayerPrefs.GetString("ServicoCliente" + 87 + "/" + dias + "");
        txtServico[88] = "" + PlayerPrefs.GetString("ServicoCliente" + 88 + "/" + dias + "");
        txtServico[89] = "" + PlayerPrefs.GetString("ServicoCliente" + 89 + "/" + dias + "");
        txtServico[90] = "" + PlayerPrefs.GetString("ServicoCliente" + 90 + "/" + dias + "");
        txtServico[91] = "" + PlayerPrefs.GetString("ServicoCliente" + 91 + "/" + dias + "");
        txtServico[92] = "" + PlayerPrefs.GetString("ServicoCliente" + 92 + "/" + dias + "");
        txtServico[93] = "" + PlayerPrefs.GetString("ServicoCliente" + 93 + "/" + dias + "");
        txtServico[94] = "" + PlayerPrefs.GetString("ServicoCliente" + 94 + "/" + dias + "");
        txtServico[95] = "" + PlayerPrefs.GetString("ServicoCliente" + 95 + "/" + dias + "");
        txtServico[96] = "" + PlayerPrefs.GetString("ServicoCliente" + 96 + "/" + dias + "");
        txtServico[97] = "" + PlayerPrefs.GetString("ServicoCliente" + 97 + "/" + dias + "");
        txtServico[98] = "" + PlayerPrefs.GetString("ServicoCliente" + 98 + "/" + dias + "");
        txtServico[99] = "" + PlayerPrefs.GetString("ServicoCliente" + 99 + "/" + dias + "");

        txtObservacao[0] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 0 + "/" + dias + "");
        txtObservacao[1] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 1 + "/" + dias + "");
        txtObservacao[2] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 2 + "/" + dias + "");
        txtObservacao[3] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 3 + "/" + dias + "");
        txtObservacao[4] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 4 + "/" + dias + "");
        txtObservacao[5] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 5 + "/" + dias + "");
        txtObservacao[6] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 6 + "/" + dias + "");
        txtObservacao[7] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 7 + "/" + dias + "");
        txtObservacao[8] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 8 + "/" + dias + "");
        txtObservacao[9] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 9 + "/" + dias + "");
        txtObservacao[10] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 10 + "/" + dias + "");
        txtObservacao[11] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 11 + "/" + dias + "");
        txtObservacao[12] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 12 + "/" + dias + "");
        txtObservacao[13] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 13 + "/" + dias + "");
        txtObservacao[14] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 14 + "/" + dias + "");
        txtObservacao[15] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 15 + "/" + dias + "");
        txtObservacao[16] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 16 + "/" + dias + "");
        txtObservacao[17] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 17 + "/" + dias + "");
        txtObservacao[18] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 18 + "/" + dias + "");
        txtObservacao[19] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 19 + "/" + dias + "");
        txtObservacao[20] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 20 + "/" + dias + "");
        txtObservacao[21] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 21 + "/" + dias + "");
        txtObservacao[22] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 22 + "/" + dias + "");
        txtObservacao[23] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 23 + "/" + dias + "");
        txtObservacao[24] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 24 + "/" + dias + "");

        txtObservacao[25] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 25 + "/" + dias + "");
        txtObservacao[26] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 26 + "/" + dias + "");
        txtObservacao[27] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 27 + "/" + dias + "");
        txtObservacao[28] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 28 + "/" + dias + "");
        txtObservacao[29] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 29 + "/" + dias + "");
        txtObservacao[30] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 30 + "/" + dias + "");
        txtObservacao[31] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 31 + "/" + dias + "");
        txtObservacao[32] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 32 + "/" + dias + "");
        txtObservacao[33] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 33 + "/" + dias + "");
        txtObservacao[34] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 34 + "/" + dias + "");
        txtObservacao[35] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 35 + "/" + dias + "");
        txtObservacao[36] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 36 + "/" + dias + "");
        txtObservacao[37] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 37 + "/" + dias + "");
        txtObservacao[38] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 38 + "/" + dias + "");
        txtObservacao[39] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 39 + "/" + dias + "");
        txtObservacao[40] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 40 + "/" + dias + "");
        txtObservacao[41] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 41 + "/" + dias + "");
        txtObservacao[42] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 42 + "/" + dias + "");
        txtObservacao[43] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 43 + "/" + dias + "");
        txtObservacao[44] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 44 + "/" + dias + "");
        txtObservacao[45] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 45 + "/" + dias + "");
        txtObservacao[46] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 46 + "/" + dias + "");
        txtObservacao[47] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 47 + "/" + dias + "");
        txtObservacao[48] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 48 + "/" + dias + "");
        txtObservacao[49] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 49 + "/" + dias + "");

        txtObservacao[50] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 50 + "/" + dias + "");
        txtObservacao[51] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 51 + "/" + dias + "");
        txtObservacao[52] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 52 + "/" + dias + "");
        txtObservacao[53] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 53 + "/" + dias + "");
        txtObservacao[54] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 54 + "/" + dias + "");
        txtObservacao[55] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 55 + "/" + dias + "");
        txtObservacao[56] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 56 + "/" + dias + "");
        txtObservacao[57] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 57 + "/" + dias + "");
        txtObservacao[58] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 58 + "/" + dias + "");
        txtObservacao[59] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 59 + "/" + dias + "");
        txtObservacao[60] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 60 + "/" + dias + "");
        txtObservacao[61] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 61 + "/" + dias + "");
        txtObservacao[62] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 62 + "/" + dias + "");
        txtObservacao[63] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 63 + "/" + dias + "");
        txtObservacao[64] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 64 + "/" + dias + "");
        txtObservacao[65] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 65 + "/" + dias + "");
        txtObservacao[66] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 66 + "/" + dias + "");
        txtObservacao[67] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 67 + "/" + dias + "");
        txtObservacao[68] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 68 + "/" + dias + "");
        txtObservacao[69] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 69 + "/" + dias + "");
        txtObservacao[70] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 70 + "/" + dias + "");
        txtObservacao[71] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 71 + "/" + dias + "");
        txtObservacao[72] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 72 + "/" + dias + "");
        txtObservacao[73] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 73 + "/" + dias + "");
        txtObservacao[74] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 74 + "/" + dias + "");

        txtObservacao[75] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 75 + "/" + dias + "");
        txtObservacao[76] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 76 + "/" + dias + "");
        txtObservacao[77] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 77 + "/" + dias + "");
        txtObservacao[78] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 78 + "/" + dias + "");
        txtObservacao[79] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 79 + "/" + dias + "");
        txtObservacao[80] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 80 + "/" + dias + "");
        txtObservacao[81] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 81 + "/" + dias + "");
        txtObservacao[82] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 82 + "/" + dias + "");
        txtObservacao[83] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 83 + "/" + dias + "");
        txtObservacao[84] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 84 + "/" + dias + "");
        txtObservacao[85] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 85 + "/" + dias + "");
        txtObservacao[86] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 86 + "/" + dias + "");
        txtObservacao[87] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 87 + "/" + dias + "");
        txtObservacao[88] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 88 + "/" + dias + "");
        txtObservacao[89] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 89 + "/" + dias + "");
        txtObservacao[90] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 90 + "/" + dias + "");
        txtObservacao[91] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 91 + "/" + dias + "");
        txtObservacao[92] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 92 + "/" + dias + "");
        txtObservacao[93] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 93 + "/" + dias + "");
        txtObservacao[94] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 94 + "/" + dias + "");
        txtObservacao[95] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 95 + "/" + dias + "");
        txtObservacao[96] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 96 + "/" + dias + "");
        txtObservacao[97] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 97 + "/" + dias + "");
        txtObservacao[98] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 98 + "/" + dias + "");
        txtObservacao[99] = "" + PlayerPrefs.GetString("ObservacaoCliente" + 99 + "/" + dias + "");
    }

    public void CarregarDadosCliente()
    {
        txtButtonCliente[0].text = "" + PlayerPrefs.GetString("NomeCliente" + 0 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 0 + "/" + dias + "");
        txtButtonCliente[1].text = "" + PlayerPrefs.GetString("NomeCliente" + 1 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 1 + "/" + dias + "");
        txtButtonCliente[2].text = "" + PlayerPrefs.GetString("NomeCliente" + 2 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 2 + "/" + dias + "");
        txtButtonCliente[3].text = "" + PlayerPrefs.GetString("NomeCliente" + 3 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 3 + "/" + dias + "");
        txtButtonCliente[4].text = "" + PlayerPrefs.GetString("NomeCliente" + 4 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 4 + "/" + dias + "");
        txtButtonCliente[5].text = "" + PlayerPrefs.GetString("NomeCliente" + 5 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 5 + "/" + dias + "");
        txtButtonCliente[6].text = "" + PlayerPrefs.GetString("NomeCliente" + 6 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 6 + "/" + dias + "");
        txtButtonCliente[7].text = "" + PlayerPrefs.GetString("NomeCliente" + 7 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 7 + "/" + dias + "");
        txtButtonCliente[8].text = "" + PlayerPrefs.GetString("NomeCliente" + 8 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 8 + "/" + dias + "");
        txtButtonCliente[9].text = "" + PlayerPrefs.GetString("NomeCliente" + 9 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 9 + "/" + dias + "");
        txtButtonCliente[10].text = "" + PlayerPrefs.GetString("NomeCliente" + 10 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 10 + "/" + dias + "");
        txtButtonCliente[11].text = "" + PlayerPrefs.GetString("NomeCliente" + 11 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 11 + "/" + dias + "");
        txtButtonCliente[12].text = "" + PlayerPrefs.GetString("NomeCliente" + 12 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 12 + "/" + dias + "");
        txtButtonCliente[13].text = "" + PlayerPrefs.GetString("NomeCliente" + 13 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 13 + "/" + dias + "");
        txtButtonCliente[14].text = "" + PlayerPrefs.GetString("NomeCliente" + 14 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 14 + "/" + dias + "");
        txtButtonCliente[15].text = "" + PlayerPrefs.GetString("NomeCliente" + 15 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 15 + "/" + dias + "");
        txtButtonCliente[16].text = "" + PlayerPrefs.GetString("NomeCliente" + 16 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 16 + "/" + dias + "");
        txtButtonCliente[17].text = "" + PlayerPrefs.GetString("NomeCliente" + 17 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 17 + "/" + dias + "");
        txtButtonCliente[18].text = "" + PlayerPrefs.GetString("NomeCliente" + 18 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 18 + "/" + dias + "");
        txtButtonCliente[19].text = "" + PlayerPrefs.GetString("NomeCliente" + 19 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 19 + "/" + dias + "");
        txtButtonCliente[20].text = "" + PlayerPrefs.GetString("NomeCliente" + 20 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 20 + "/" + dias + "");
        txtButtonCliente[21].text = "" + PlayerPrefs.GetString("NomeCliente" + 21 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 21 + "/" + dias + "");
        txtButtonCliente[22].text = "" + PlayerPrefs.GetString("NomeCliente" + 22 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 22 + "/" + dias + "");
        txtButtonCliente[23].text = "" + PlayerPrefs.GetString("NomeCliente" + 23 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 23 + "/" + dias + "");
        txtButtonCliente[24].text = "" + PlayerPrefs.GetString("NomeCliente" + 24 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 24 + "/" + dias + "");
        txtButtonCliente[25].text = "" + PlayerPrefs.GetString("NomeCliente" + 25 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 25 + "/" + dias + "");
        txtButtonCliente[26].text = "" + PlayerPrefs.GetString("NomeCliente" + 26 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 26 + "/" + dias + "");
        txtButtonCliente[27].text = "" + PlayerPrefs.GetString("NomeCliente" + 27 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 27 + "/" + dias + "");
        txtButtonCliente[28].text = "" + PlayerPrefs.GetString("NomeCliente" + 28 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 28 + "/" + dias + "");
        txtButtonCliente[29].text = "" + PlayerPrefs.GetString("NomeCliente" + 29 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 29 + "/" + dias + "");
        txtButtonCliente[30].text = "" + PlayerPrefs.GetString("NomeCliente" + 30 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 30 + "/" + dias + "");
        txtButtonCliente[31].text = "" + PlayerPrefs.GetString("NomeCliente" + 31 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 31 + "/" + dias + "");
        txtButtonCliente[32].text = "" + PlayerPrefs.GetString("NomeCliente" + 32 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 32 + "/" + dias + "");
        txtButtonCliente[33].text = "" + PlayerPrefs.GetString("NomeCliente" + 33 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 33 + "/" + dias + "");
        txtButtonCliente[34].text = "" + PlayerPrefs.GetString("NomeCliente" + 34 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 34 + "/" + dias + "");
        txtButtonCliente[35].text = "" + PlayerPrefs.GetString("NomeCliente" + 35 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 35 + "/" + dias + "");
        txtButtonCliente[36].text = "" + PlayerPrefs.GetString("NomeCliente" + 36 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 36 + "/" + dias + "");
        txtButtonCliente[37].text = "" + PlayerPrefs.GetString("NomeCliente" + 37 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 37 + "/" + dias + "");
        txtButtonCliente[38].text = "" + PlayerPrefs.GetString("NomeCliente" + 38 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 38 + "/" + dias + "");
        txtButtonCliente[39].text = "" + PlayerPrefs.GetString("NomeCliente" + 39 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 39 + "/" + dias + "");
        txtButtonCliente[40].text = "" + PlayerPrefs.GetString("NomeCliente" + 40 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 40 + "/" + dias + "");
        txtButtonCliente[41].text = "" + PlayerPrefs.GetString("NomeCliente" + 41 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 41 + "/" + dias + "");
        txtButtonCliente[42].text = "" + PlayerPrefs.GetString("NomeCliente" + 42 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 42 + "/" + dias + "");
        txtButtonCliente[43].text = "" + PlayerPrefs.GetString("NomeCliente" + 43 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 43 + "/" + dias + "");
        txtButtonCliente[44].text = "" + PlayerPrefs.GetString("NomeCliente" + 44 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 44 + "/" + dias + "");
        txtButtonCliente[45].text = "" + PlayerPrefs.GetString("NomeCliente" + 45 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 45 + "/" + dias + "");
        txtButtonCliente[46].text = "" + PlayerPrefs.GetString("NomeCliente" + 46 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 46 + "/" + dias + "");
        txtButtonCliente[47].text = "" + PlayerPrefs.GetString("NomeCliente" + 47 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 47 + "/" + dias + "");
        txtButtonCliente[48].text = "" + PlayerPrefs.GetString("NomeCliente" + 48 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 48 + "/" + dias + "");
        txtButtonCliente[49].text = "" + PlayerPrefs.GetString("NomeCliente" + 49 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 49 + "/" + dias + "");
        txtButtonCliente[50].text = "" + PlayerPrefs.GetString("NomeCliente" + 50 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 50 + "/" + dias + "");
        txtButtonCliente[51].text = "" + PlayerPrefs.GetString("NomeCliente" + 51 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 51 + "/" + dias + "");
        txtButtonCliente[52].text = "" + PlayerPrefs.GetString("NomeCliente" + 52 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 52 + "/" + dias + "");
        txtButtonCliente[53].text = "" + PlayerPrefs.GetString("NomeCliente" + 53 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 53 + "/" + dias + "");
        txtButtonCliente[54].text = "" + PlayerPrefs.GetString("NomeCliente" + 54 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 54 + "/" + dias + "");
        txtButtonCliente[55].text = "" + PlayerPrefs.GetString("NomeCliente" + 55 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 55 + "/" + dias + "");
        txtButtonCliente[56].text = "" + PlayerPrefs.GetString("NomeCliente" + 56 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 56 + "/" + dias + "");
        txtButtonCliente[57].text = "" + PlayerPrefs.GetString("NomeCliente" + 57 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 57 + "/" + dias + "");
        txtButtonCliente[58].text = "" + PlayerPrefs.GetString("NomeCliente" + 58 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 58 + "/" + dias + "");
        txtButtonCliente[59].text = "" + PlayerPrefs.GetString("NomeCliente" + 59 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 59 + "/" + dias + "");
        txtButtonCliente[60].text = "" + PlayerPrefs.GetString("NomeCliente" + 60 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 60 + "/" + dias + "");
        txtButtonCliente[61].text = "" + PlayerPrefs.GetString("NomeCliente" + 61 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 61 + "/" + dias + "");
        txtButtonCliente[62].text = "" + PlayerPrefs.GetString("NomeCliente" + 62 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 62 + "/" + dias + "");
        txtButtonCliente[63].text = "" + PlayerPrefs.GetString("NomeCliente" + 63 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 63 + "/" + dias + "");
        txtButtonCliente[64].text = "" + PlayerPrefs.GetString("NomeCliente" + 64 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 64 + "/" + dias + "");
        txtButtonCliente[65].text = "" + PlayerPrefs.GetString("NomeCliente" + 65 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 65 + "/" + dias + "");
        txtButtonCliente[66].text = "" + PlayerPrefs.GetString("NomeCliente" + 66 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 66 + "/" + dias + "");
        txtButtonCliente[67].text = "" + PlayerPrefs.GetString("NomeCliente" + 67 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 67 + "/" + dias + "");
        txtButtonCliente[68].text = "" + PlayerPrefs.GetString("NomeCliente" + 68 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 68 + "/" + dias + "");
        txtButtonCliente[69].text = "" + PlayerPrefs.GetString("NomeCliente" + 69 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 69 + "/" + dias + "");
        txtButtonCliente[70].text = "" + PlayerPrefs.GetString("NomeCliente" + 70 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 70 + "/" + dias + "");
        txtButtonCliente[71].text = "" + PlayerPrefs.GetString("NomeCliente" + 71 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 71 + "/" + dias + "");
        txtButtonCliente[72].text = "" + PlayerPrefs.GetString("NomeCliente" + 72 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 72 + "/" + dias + "");
        txtButtonCliente[73].text = "" + PlayerPrefs.GetString("NomeCliente" + 73 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 73 + "/" + dias + "");
        txtButtonCliente[74].text = "" + PlayerPrefs.GetString("NomeCliente" + 74 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 74 + "/" + dias + "");
        txtButtonCliente[75].text = "" + PlayerPrefs.GetString("NomeCliente" + 75 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 75 + "/" + dias + "");
        txtButtonCliente[76].text = "" + PlayerPrefs.GetString("NomeCliente" + 76 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 76 + "/" + dias + "");
        txtButtonCliente[77].text = "" + PlayerPrefs.GetString("NomeCliente" + 77 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 77 + "/" + dias + "");
        txtButtonCliente[78].text = "" + PlayerPrefs.GetString("NomeCliente" + 78 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 78 + "/" + dias + "");
        txtButtonCliente[79].text = "" + PlayerPrefs.GetString("NomeCliente" + 79 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 79 + "/" + dias + "");
        txtButtonCliente[80].text = "" + PlayerPrefs.GetString("NomeCliente" + 80 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 80 + "/" + dias + "");
        txtButtonCliente[81].text = "" + PlayerPrefs.GetString("NomeCliente" + 81 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 81 + "/" + dias + "");
        txtButtonCliente[82].text = "" + PlayerPrefs.GetString("NomeCliente" + 82 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 82 + "/" + dias + "");
        txtButtonCliente[83].text = "" + PlayerPrefs.GetString("NomeCliente" + 83 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 83 + "/" + dias + "");
        txtButtonCliente[84].text = "" + PlayerPrefs.GetString("NomeCliente" + 84 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 84 + "/" + dias + "");
        txtButtonCliente[85].text = "" + PlayerPrefs.GetString("NomeCliente" + 85 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 85 + "/" + dias + "");
        txtButtonCliente[86].text = "" + PlayerPrefs.GetString("NomeCliente" + 86 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 86 + "/" + dias + "");
        txtButtonCliente[87].text = "" + PlayerPrefs.GetString("NomeCliente" + 87 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 87 + "/" + dias + "");
        txtButtonCliente[88].text = "" + PlayerPrefs.GetString("NomeCliente" + 88 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 88 + "/" + dias + "");
        txtButtonCliente[89].text = "" + PlayerPrefs.GetString("NomeCliente" + 89 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 89 + "/" + dias + "");
        txtButtonCliente[90].text = "" + PlayerPrefs.GetString("NomeCliente" + 90 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 90 + "/" + dias + "");
        txtButtonCliente[91].text = "" + PlayerPrefs.GetString("NomeCliente" + 91 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 91 + "/" + dias + "");
        txtButtonCliente[92].text = "" + PlayerPrefs.GetString("NomeCliente" + 92 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 92 + "/" + dias + "");
        txtButtonCliente[93].text = "" + PlayerPrefs.GetString("NomeCliente" + 93 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 93 + "/" + dias + "");
        txtButtonCliente[94].text = "" + PlayerPrefs.GetString("NomeCliente" + 94 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 94 + "/" + dias + "");
        txtButtonCliente[95].text = "" + PlayerPrefs.GetString("NomeCliente" + 95 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 95 + "/" + dias + "");
        txtButtonCliente[96].text = "" + PlayerPrefs.GetString("NomeCliente" + 96 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 96 + "/" + dias + "");
        txtButtonCliente[97].text = "" + PlayerPrefs.GetString("NomeCliente" + 97 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 97 + "/" + dias + "");
        txtButtonCliente[98].text = "" + PlayerPrefs.GetString("NomeCliente" + 98 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 98 + "/" + dias + "");
        txtButtonCliente[99].text = "" + PlayerPrefs.GetString("NomeCliente" + 99 + "/" + dias + "") + " / " + PlayerPrefs.GetString("ServicoCliente" + 99 + "/" + dias + "");
    }
}
