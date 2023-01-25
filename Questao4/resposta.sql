-- o assunto, o ano e a quantidade de ocorrências, filtrando apenas assuntos que tenham mais de 3 ocorrências no mesmo ano
-- ordenar os registros por ANO e por QUANTIDADE de ocorrências de forma DECRESCENTE

SELECT assunto, ano, COUNT(assunto) AS quantidade
FROM atendimentos
GROUP BY ano, assunto
HAVING quantidade > 3
ORDER BY ano DESC, quantidade DESC;