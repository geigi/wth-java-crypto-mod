#!/bin/bash

check_vars() {
  for VAR in ${VARS[@]}
  do
    if [ -z ${!VAR} ]; then echo "\$${VAR} is missing"; exit 1; fi
  done
}

# check requirements
REQS=( curl jq unzip )

for REQ in ${REQS[@]}
do
  which ${REQ} >/dev/null
  if [ ! $? -eq 0 ]; then
    echo "requirement ${REQ} is missing"
    exit 1
  fi
done

# set vars in project settings
BASE_URL=${BASE_URL:-$(echo $CI_PROJECT_URL |  cut -d'/' -f1-3)}
VARS=( BASE_URL PRIVATE_TOKEN PROJECT STAGE )

check_vars

# fetch last successful build

DEFAULT_FILTER=".[] | select(.status==\"success\")  | select(.stage==\"${STAGE}\") | select(.name==\"${JOB_NAME}\")"
FILTER="$DEFAULT_FILTER"

if [ ! -z $COMMIT ]; then
  FILTER+=" | select(.commit.short_id==\"$COMMIT\")"
fi

LAST_SUCCESSFUL_BUILD=$(curl -s -H "PRIVATE-TOKEN: ${PRIVATE_TOKEN}" "${BASE_URL}/api/v4/projects/${PROJECT}/jobs?per_page=${PER_PAGE:-50}" | jq -c ''"$FILTER"' | {id}' | head -n1 | grep -oE '[0-9]+')

echo "${LAST_SUCCESSFUL_BUILD}"

[ -z $OUT_FILE ] && OUT_FILE="artifacts.zip"
curl -fksSL -o ${OUT_FILE} -H "PRIVATE-TOKEN: ${PRIVATE_TOKEN}" "${BASE_URL}/api/v4/projects/${PROJECT}/jobs/${LAST_SUCCESSFUL_BUILD}/artifacts"